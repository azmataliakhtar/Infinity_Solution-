Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports INF.Web.Data
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Partial Class PaymentSuccess
    Inherits System.Web.UI.Page

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Structure EmailSettings
        Dim EmailSender As String
        Dim EmailServer As String
        Dim EmailAuthenticationUser As String
        Dim EmailAuthenticationPassword As String
        Dim ApplicationServerURL As String
        Dim FeedbackEmail As String
    End Structure
    Structure CustomerInfo
        Dim FirstName, LastName As String
        Dim Address As String
        Dim City, PostCode As String
        Dim Email As String
        Dim Telephone As String
        Dim Mobile As String
    End Structure

    Dim SqlCon As SqlConnection
    Dim EmilSet As EmailSettings
    Dim CustInfo As CustomerInfo

    Private Sub EmptyCart()
        Try

            If (Not Request.Cookies("ShoppingCart") Is Nothing) Then
                Dim myCookie As HttpCookie
                myCookie = New HttpCookie("ShoppingCart")
                myCookie.Expires = DateTime.Now.AddYears(-1)
                Response.Cookies.Add(myCookie)

                Session("SpecInstr") = Nothing
                Session("IsCartEmpty") = True
                Exit Sub
            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Session("IsCartEmpty") Is Nothing Then
                If Session("IsCartEmpty") = False And CheckIfOrderisPaid() Then

                    'Send emails here
                    If GetEmailSettings() Then
                        GetAndSendOrderEmails()
                    End If

                    EmptyCart()

                Else
                    Response.RedirectTo("Error.aspx")
                    Exit Sub
                End If
            Else
                Response.RedirectTo("Error.aspx")
                Exit Sub
            End If

            EmptyCart()

        Catch ex As Exception
            _log.Error(ex)
            EmptyCart()
            Response.RedirectTo("Error.aspx")
        End Try

    End Sub

    Private Function GetEmailSettings() As Boolean
        Try

            Dim sqlcmd As New SqlCommand
            Dim sqlread As SqlDataReader

            If SqlCon Is Nothing Then
                SqlCon = New SqlConnection(ConfigurationManager.ConnectionStrings("PizzaWebConnectionString").ConnectionString)
            End If

            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.CommandText = "GetEmailSettings"

            If Not SqlCon Is Nothing Then
                If SqlCon.State <> ConnectionState.Open Then
                    SqlCon.Open()
                End If

                If SqlCon.State = ConnectionState.Open Then
                    sqlcmd.Connection = SqlCon
                    sqlread = sqlcmd.ExecuteReader

                    If sqlread.HasRows Then
                        sqlread.Read()
                        If Not IsDBNull(sqlread.GetValue(1)) Then
                            EmilSet.EmailSender = sqlread.GetValue(1)
                        End If
                        If Not IsDBNull(sqlread.GetValue(2)) Then
                            EmilSet.EmailServer = sqlread.GetValue(2)
                        End If
                        If Not IsDBNull(sqlread.GetValue(3)) Then
                            EmilSet.EmailAuthenticationUser = sqlread.GetValue(3)
                        End If
                        If Not IsDBNull(sqlread.GetValue(4)) Then
                            EmilSet.EmailAuthenticationPassword = sqlread.GetValue(4)
                        End If
                        If Not IsDBNull(sqlread.GetValue(5)) Then
                            EmilSet.ApplicationServerURL = sqlread.GetValue(5)
                        End If
                        If Not IsDBNull(sqlread.GetValue(6)) Then
                            EmilSet.FeedbackEmail = sqlread.GetValue(6)
                        End If
                    Else
                        sqlread.Close()
                        EmilSet = Nothing
                        Return False
                    End If
                    sqlread.Close()
                End If
            End If
            Return True

        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Function

    Private Function CheckIfOrderisPaid() As Boolean

        Dim bSendEmail As Boolean = False

        Try

            Dim sqlcmd As New SqlCommand
            Dim sqlread As SqlDataReader
            Dim orderid As Integer
            Dim Order_Id As String = Request.QueryString("Order_Id")

            If Order_Id.Length > 0 Then
                orderid = CryptoUtility.DecryptText(Order_Id)
            Else
                Return False
            End If

            If SqlCon Is Nothing Then
                SqlCon = New SqlConnection(ConfigurationManager.ConnectionStrings("PizzaWebConnectionString").ConnectionString)
            End If

            If Not SqlCon Is Nothing Then
                If SqlCon.State <> ConnectionState.Open Then
                    SqlCon.Open()
                End If

                If SqlCon.State = ConnectionState.Open Then
                    sqlcmd.Connection = SqlCon
                    sqlcmd.CommandType = CommandType.Text
                    sqlcmd.CommandText = "Select PayStatus from orderinfo where OrderID=" & orderid & ""
                    sqlread = sqlcmd.ExecuteReader

                    If sqlread.HasRows Then
                        'Record found, so send email
                        bSendEmail = True
                    Else
                        bSendEmail = False
                    End If
                    sqlread.Close()
                End If
            End If

            Return bSendEmail

        Catch ex As Exception
            _log.Error(ex)
            Return bSendEmail
        End Try
    End Function
    Protected Sub GetAndSendOrderEmails()
        Try

            Dim OrderItems As String = ""
            Dim OrderItemsQty As String = ""
            Dim OrderItemsPrice As String = ""
            Dim OrderItemsDressing As String = ""
            Dim TotalOrdePrice As Double = 0

            ' To Make sure the cookie is not empty
            If Request.Cookies("ShoppingCart") IsNot Nothing Then

                Dim oCookie As HttpCookie = DirectCast(Request.Cookies("ShoppingCart"), HttpCookie)

                Dim sProductID As String = oCookie.Value.ToString()
                Dim dt_final As New DataTable()

                ' which means the user remove all products and he is adding a new product
                ' in this case i need to remove the ",". otherwise the '' will be considered as item.
                If sProductID.IndexOf(",") = 0 Then
                    sProductID = sProductID.Remove(0, 1)
                End If

                If sProductID <> "" Then
                    Dim sep As Char() = {","c}
                    ' split the cookie values into array
                    Dim sArrProdID As String() = sProductID.Split(sep)

                    'create datatable for purchased items
                    Dim dt As New DataTable()
                    dt.Columns.Add(New DataColumn("Counter"))
                    dt.Columns.Add(New DataColumn("ProductID"))

                    dt.Columns.Add(New DataColumn("ProductName"))
                    dt.Columns.Add(New DataColumn("Issue")) 'Quantity
                    dt.Columns.Add(New DataColumn(("prod_price"), System.Type.[GetType]("System.Decimal")))

                    ' to map the values from  array of string(sArrProdID) to datatable
                    Dim counter As Integer = 1
                    Dim i As Integer = 0
                    While i < sArrProdID.Length - 1
                        Dim dr As DataRow = dt.NewRow()
                        dr("Counter") = counter
                        dr("ProductID") = sArrProdID(i)

                        dr("ProductName") = sArrProdID(i + 1)
                        dr("Issue") = 1
                        dr("prod_price") = sArrProdID(i + 2)
                        dt.Rows.Add(dr)
                        counter += 1
                        i = i + 3
                    End While

                    'temp table to return the distinct values only
                    Dim dtTemp As New DataTable()
                    Dim col As String() = {"ProductID", "ProductName", "prod_price"}
                    dtTemp = dt.DefaultView.ToTable(True, col)

                    dt_final = dt.Clone()

                    'to calculate the number of issued items
                    counter = 1
                    For Each dr As DataRow In dtTemp.Rows
                        Dim dr_final As DataRow = dt_final.NewRow()
                        dr_final("ProductID") = dr("ProductID")

                        dr_final("ProductName") = dr("ProductName")
                        OrderItems = OrderItems & dr_final("ProductName") & ","

                        dr_final("Issue") = dt.Compute("count(ProductName)", "ProductName='" + dr("ProductName") & "'").ToString()
                        OrderItemsQty = OrderItemsQty & dr_final("Issue") & ","

                        dr_final("Counter") = counter
                        dr_final("prod_price") = dt.Compute("sum(prod_price)", "ProductName='" + dr("ProductName") & "'")
                        OrderItemsPrice = OrderItemsPrice & dr_final("prod_price") & ","

                        dt_final.Rows.Add(dr_final)
                        counter += 1

                        'Calculate Total Price Here
                        TotalOrdePrice = TotalOrdePrice + CDbl(dr_final("prod_price"))

                    Next
                    TotalOrdePrice = FormatNumber(TotalOrdePrice, 2)

                    'Here we can try to add online discount 10%
                    Dim dr_finalDiscount As DataRow = dt_final.NewRow()
                    dr_finalDiscount("ProductID") = 999

                    dr_finalDiscount("ProductName") = "ONLINE DISCOUNT"
                    OrderItems = OrderItems & dr_finalDiscount("ProductName") & ","

                    dr_finalDiscount("Issue") = "1"
                    OrderItemsQty = OrderItemsQty & dr_finalDiscount("Issue") & ","

                    dr_finalDiscount("Counter") = counter
                    dr_finalDiscount("prod_price") = (TotalOrdePrice * 10 / 100) * (-1)
                    OrderItemsPrice = OrderItemsPrice & dr_finalDiscount("prod_price") & ","

                    TotalOrdePrice = TotalOrdePrice + dr_finalDiscount("prod_price")

                    dt_final.Rows.Add(dr_finalDiscount)
                    counter += 1

                End If

                SendOrderEmail2Customer(Session("Email").ToString(), "", "", EmilSet.EmailServer, OrderItems, OrderItemsQty, OrderItemsPrice, OrderItemsDressing, TotalOrdePrice)
                SendOrderEmail2Restaurant(EmilSet.FeedbackEmail, "", "info@infinitysol.co.uk", EmilSet.EmailServer, OrderItems, OrderItemsQty, OrderItemsPrice, OrderItemsDressing, TotalOrdePrice)

            End If


        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo("Error.aspx")
        End Try
    End Sub

    Private Sub SendOrderEmail2Customer(ByVal ToList As String, ByVal CCList As String, ByVal BCCList As String, ByVal SMTPAddress As String, ByVal OrderItems As String, ByVal OrderItemsQty As String, ByVal OrderItemsPrice As String, ByVal OrderItemsDressing As String, ByVal TotalOrdePrice As Double)

        Try


            Dim temp() As String
            Dim fromAddress As New MailAddress(EmilSet.EmailSender)

            temp = ToList.Split(";")

            Dim mail As New MailMessage()
            mail.From = fromAddress

            For Each Id As String In temp
                If Id <> "" Then
                    mail.To.Add(Id)
                End If
            Next

            If CCList IsNot Nothing AndAlso CCList <> String.Empty Then
                temp = CCList.Split(";")
                For Each Id As [String] In temp
                    If Id <> "" Then
                        mail.CC.Add(Id)
                    End If
                Next
            End If

            If BCCList IsNot Nothing AndAlso BCCList <> String.Empty Then
                temp = BCCList.Split(";")
                For Each Id As String In temp
                    If Id <> "" Then
                        mail.Bcc.Add(Id)
                    End If
                Next
            End If


            mail.Subject = "Your Order at Wrapidos Leicester"
            mail.BodyEncoding = Encoding.Default
            mail.IsBodyHtml = True


            mail.Body += "<html><body>"

            mail.Body += "<font face='Arial'>Date: " & DateTime.Now.ToShortDateString() & "</font><br>"

            mail.Body += "<h4><font face='Arial'>Dear " & "Valued Customer," & "</font></h4><br>"

            mail.Body += "<h4><u><font face='Arial'>Thanks for Placing order with Wrapidos Leicester </font></u></h4>"

            mail.Body += "<font face='Arial'>We have received your online order and currently processing it.<br> Please be patient, we are trying our best to serve you as soon as possible.</font><br><br>"

            If Session("OrderType") = "COLLECTION" Then
                mail.Body += "<font face='Arial'>Please collect the order at address given at end of this email in 45 minutes, We appriciate your patience. !</font><br><br>"
            Else
                mail.Body += "<font face='Arial'>Your order will be delivered to you in 45 minutes, We appriciate your patience. !</font><br><br>"
            End If

            mail.Body += "<font face='Arial'>Here is what you have ordered !</font><br>"

            mail.Body += "<font face='Arial'><table border=1><tr><td><b>Qty</b></td>"
            mail.Body += "<td><b>Item</b></td>"
            mail.Body += "<td><b>Price</b></td></tr><br><br>"

            Dim OrdQty(), OrdItems(), OrdItemPrice() As String
            Dim i As Integer = 0
            OrdQty = OrderItemsQty.Split(",")
            OrdItems = OrderItems.Split(",")
            OrdItemPrice = OrderItemsPrice.Split(",")
            If Not OrdQty Is Nothing Then
                For i = 0 To OrdQty.Length - 1
                    If OrdQty(i) <> "" Then
                        mail.Body += "<tr><td>" & OrdQty(i) & "</td><td>" & OrdItems(i) & "</td><td>" & FormatNumber(OrdItemPrice(i), 2) & "</td></tr>"
                    End If
                Next
            End If

            mail.Body += "<tr><td>" & "" & "</td><td><b>" & "Payment Processing Charges " & "</b></td><td><b>" & FormatNumber(Session("PaymentCharges"), 2) & "</b></td></tr>"

            TotalOrdePrice = TotalOrdePrice + CDbl(Session("PaymentCharges"))

            mail.Body += "<tr><td>" & "" & "</td><td><b>" & "Total Amount" & "</b></td><td><b>" & FormatNumber(TotalOrdePrice, 2) & "</b></td></tr></table>"
            mail.Body += " <br/><br/>"

            mail.Body += "<font face='Arial'>Please contact us if something is not right.</font><br><br>"
            mail.Body += "<font face='Arial'>Thanks for your order, Please call again.</font><br><br><br><br>"


            mail.Body += "<font face='Arial'>Best Regards</font><br><br>"
            mail.Body += "<font face='Arial'><b>Wrapidos Leicester Team</b><br></font>"
            mail.Body += "<font face='Arial'>97A London Road</font><br>"
            mail.Body += "<font face='Arial'>Leicester, LE2 0QS</font><br>"
            mail.Body += "<font face='Arial'>Tel: +44(0)1162554079</font><br>"
            mail.Body += "<font face='Arial'>E-mail: Sales@wrapidos.co.uk</font><br>"

            mail.Body += "</body></html>"

            Dim SmtpMail As New SmtpClient()
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = New System.Net.NetworkCredential(EmilSet.EmailAuthenticationUser, EmilSet.EmailAuthenticationPassword)
            SmtpMail.Host = SMTPAddress
            SmtpMail.Send(mail)

        Catch ex As Exception
            _log.Error(ex)
        End Try

    End Sub

    Private Sub SendOrderEmail2Restaurant(ByVal ToList As String, ByVal CCList As String, ByVal BCCList As String, ByVal SMTPAddress As String, ByVal OrderItems As String, ByVal OrderItemsQty As String, ByVal OrderItemsPrice As String, ByVal OrderItemsDressing As String, ByVal TotalOrdePrice As Double)

        Try

            Dim temp() As String
            Dim fromAddress As New MailAddress(EmilSet.EmailSender)

            temp = ToList.Split(";")

            Dim mail As New MailMessage()
            mail.From = fromAddress

            For Each Id As String In temp
                If Id <> "" Then
                    mail.To.Add(Id)
                End If
            Next

            If CCList IsNot Nothing AndAlso CCList <> String.Empty Then
                temp = CCList.Split(";")
                For Each Id As [String] In temp
                    If Id <> "" Then
                        mail.CC.Add(Id)
                    End If
                Next
            End If

            If BCCList IsNot Nothing AndAlso BCCList <> String.Empty Then
                temp = BCCList.Split(";")
                For Each Id As String In temp
                    If Id <> "" Then
                        mail.Bcc.Add(Id)
                    End If
                Next
            End If


            mail.Subject = "New " & Session("OrderType") & " Order - Wrapidos Leicester"
            mail.BodyEncoding = Encoding.Default
            mail.IsBodyHtml = True


            mail.Body += "<html><body>"

            mail.Body += "<font face='Arial'>Date: " & DateTime.Now.ToShortDateString() & "</font><br>"

            mail.Body += "<h4><font face='Arial'>Dear " & "Sir," & "</font></h4><br>"

            mail.Body += "<h4><u><font face='Arial'>You have received new " & Session("OrderType") & " order </font></u></h4>"

            If Session("PaymentMode") = "CARD" Then
                'this is paid order
                mail.Body += "<b>Customer has <font face='Arial'><h2>PAID</h2></font> this order by " & Session("PaymentMode") & "</b> <br/>"
            Else
                'this is not paid order, driver is suppose to collect money at door step
                mail.Body += "<b>Customer has <font face='Arial'><h2>NOT PAID</h2></font> this order, please collect cash at his/her doorstep </b><br><br>"
            End If

            If Session("OrderType") = "COLLECTION" Then
                mail.Body += "<font face='Arial'>Customer will collect this order in 45 minutes. !</font><br><br>"
            Else
                If GetCustomerInfo() Then
                    mail.Body += "<font face='Arial'>Please deliver this order at following address. !</font><br><br>"

                    mail.Body += "<font face='Arial'><b>" & CustInfo.FirstName & " " & CustInfo.LastName & "</b><br></font>"
                    mail.Body += "<font face='Arial'>" & CustInfo.Address & "</font><br>"
                    mail.Body += "<font face='Arial'>" & CustInfo.City & " , " & CustInfo.PostCode & "</font><br>"
                    mail.Body += "<font face='Arial'>Tel: " & CustInfo.Telephone & "</font><br>"
                    mail.Body += "<font face='Arial'>Cell: " & CustInfo.Mobile & "</font><br>"
                    mail.Body += "<font face='Arial'>E-mail: " & CustInfo.Email & "</font><br><br>"
                End If
            End If



            mail.Body += "<font face='Arial'>Here is what customer has ordered !</font><br>"

            mail.Body += "<font face='Arial'><table border=1><tr><td><b>Qty</b></td>"
            mail.Body += "<td><b>Item</b></td>"
            mail.Body += "<td><b>Price</b></td></tr><br><br>"



            Dim OrdQty(), OrdItems(), OrdItemPrice() As String
            Dim i As Integer = 0
            OrdQty = OrderItemsQty.Split(",")
            OrdItems = OrderItems.Split(",")
            OrdItemPrice = OrderItemsPrice.Split(",")
            If Not OrdQty Is Nothing Then
                For i = 0 To OrdQty.Length - 1
                    If OrdQty(i) <> "" Then
                        mail.Body += "<tr><td>" & OrdQty(i) & "</td><td>" & OrdItems(i) & "</td><td><b>" & FormatNumber(OrdItemPrice(i), 2) & "</b></td></tr>"
                    End If
                Next
            End If

            mail.Body += "<tr><td>" & "" & "</td><td><b>" & "Total Amount" & "</b></td><td><b>" & FormatNumber(TotalOrdePrice, 2) & "</b></td></tr></table>"
            mail.Body += " <br/><br/>"

            mail.Body += "<font face='Arial'>Please contact customer if you can't fulfill the order.</font><br><br>"


            mail.Body += "<font face='Arial'>Best Regards</font><br><br>"
            mail.Body += "<font face='Arial'><b>Infinity Solutions Team</b><br></font>"


            mail.Body += "</body></html>"

            Dim SmtpMail As New SmtpClient()
            SmtpMail.UseDefaultCredentials = False
            SmtpMail.Credentials = New System.Net.NetworkCredential(EmilSet.EmailAuthenticationUser, EmilSet.EmailAuthenticationPassword)
            SmtpMail.Host = SMTPAddress
            SmtpMail.Send(mail)


        Catch ex As Exception
            _log.Error(ex)
        End Try

    End Sub

    Private Function GetCustomerInfo() As Boolean
        Try

            Dim sqlcmd As New SqlCommand
            Dim sqlread As SqlDataReader

            If SqlCon Is Nothing Then
                SqlCon = New SqlConnection(ConfigurationManager.ConnectionStrings("PizzaWebConnectionString").ConnectionString)
            End If

            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.CommandText = "GetCustomerInfo"
            sqlcmd.Parameters.AddWithValue("@CustomerId", Session("CurrentUserId"))
            sqlcmd.Parameters.AddWithValue("@AddressId", Session("AddressId"))

            If Not SqlCon Is Nothing Then
                If SqlCon.State <> ConnectionState.Open Then
                    SqlCon.Open()
                End If

                If SqlCon.State = ConnectionState.Open Then
                    sqlcmd.Connection = SqlCon
                    sqlread = sqlcmd.ExecuteReader

                    If sqlread.HasRows Then

                        sqlread.Read()

                        If Not IsDBNull(sqlread.GetValue(0)) Then
                            CustInfo.FirstName = sqlread.GetValue(0)
                        End If
                        If Not IsDBNull(sqlread.GetValue(1)) Then
                            CustInfo.LastName = sqlread.GetValue(1)
                        End If

                        If Not IsDBNull(sqlread.GetValue(2)) Then
                            CustInfo.Telephone = sqlread.GetValue(2)
                        End If
                        If Not IsDBNull(sqlread.GetValue(3)) Then
                            CustInfo.Mobile = sqlread.GetValue(3)
                        End If
                        If Not IsDBNull(sqlread.GetValue(4)) Then
                            CustInfo.Email = sqlread.GetValue(4)
                        End If

                        If Not IsDBNull(sqlread.GetValue(5)) Then
                            CustInfo.Address = sqlread.GetValue(5)
                        End If
                        If Not IsDBNull(sqlread.GetValue(6)) Then
                            CustInfo.City = sqlread.GetValue(6)
                        End If
                        If Not IsDBNull(sqlread.GetValue(7)) Then
                            CustInfo.PostCode = sqlread.GetValue(7)
                        End If
                    Else
                        sqlread.Close()
                        CustInfo = Nothing
                        Return False
                    End If
                    sqlread.Close()
                End If
            End If

            Return True

        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Function

End Class
