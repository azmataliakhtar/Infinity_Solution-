Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI

Namespace Admin
    Public Class Postcode
        Inherits AdminPage

        Private ReadOnly _log As log4net.ILog = log4net.LogManager.GetLogger(GetType(Postcode))

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                LoadAllPostcode()
            End If
        End Sub

        Private Sub btnNewPostcode_Click(sender As Object, e As EventArgs) Handles btnNewPostcode.Click
            ltrMenuItemEditorHeaderText.Text = "Add New Postcode"
            phMessage.Visible = False
            ltrMessage.Text = ""
            ShowPostcodeEditorModal()
        End Sub

        Private Sub gvPostcode_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPostcode.RowCommand
            Dim index = -1
            If Not IsNothing(e.CommandArgument) AndAlso IsNumeric(e.CommandArgument) Then
                index = CInt(e.CommandArgument)
            End If

            If index < 0 Then
                Exit Sub
            End If

            Dim itemId = CInt(gvPostcode.DataKeys(index).Value)
            Dim bizPostcode = New PostcodeBusinessLogic()

            Try
                'Dim menuItem = bizMenu.GetMenuItem(itemId)
                Select Case e.CommandName
                    Case "SelectMenuItem"
                        ltrMenuItemEditorHeaderText.Text = "Edit Existing Postcode"
                        phMessage.Visible = False
                        ltrMessage.Text = ""
                        ShowPostcodeEditorModal(itemId)
                        Exit Select
                    Case "DeleteMenuItem"
                        Dim itemToDelete = bizPostcode.GetPostcodeById(itemId)
                        If Not IsNothing(itemToDelete) Then
                            If bizPostcode.DeletePostcode(itemToDelete.ID) Then
                                LoadAllPostcode()
                                ShowMessageBox("[" & itemToDelete.PostCode & "] " & "has been deleted successfully.")
                            Else
                                ShowMessageBox("[" & itemToDelete.PostCode & "] " & "has NOT been deleted.")
                            End If
                        Else
                            ShowMessageBox("[" & itemToDelete.PostCode & "] " & "could not be found!")
                        End If
                        Exit Select

                    Case Else
                        Exit Select
                End Select
            Catch ex As Exception
                _log.Error(ex.Message & vbCrLf & ex.StackTrace)
            End Try
        End Sub

        Private Sub LoadAllPostcode()
            Try
                Dim postcodeBl As New PostcodeBusinessLogic()
                Dim result As IEnumerable(Of CsPostCodePrice) = postcodeBl.GetAllPostcodes()
                gvPostcode.DataSource = result
                gvPostcode.DataBind()

            Catch ex As Exception
                _log.Error("Message: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        End Sub

        Private Sub LoadPostcode(ByVal vId As Integer)
            Try
                Dim postcodeBl As New PostcodeBusinessLogic()
                Dim result As CsPostCodePrice = postcodeBl.GetPostcodeById(vId)
                If result IsNot Nothing Then

                    With result
                        txtPostcode.Text = .PostCode
                        Price.Text = .Price
                        MinimumOrderValue.Text = .MinOrder
                        AllowDelivery.Checked = .AllowDelivery

                        hdnPostcodeID.Value = CStr(.ID)
                    End With
                End If

            Catch ex As Exception
                _log.Error("Message: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        End Sub

        Protected Sub SaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
            If Not Page.IsValid Then
                Return
            End If

            Dim currentId As Integer = 0
            If (Not String.IsNullOrWhiteSpace(hdnPostcodeID.Value)) AndAlso IsNumeric(hdnPostcodeID.Value) Then
                currentId = CDec(hdnPostcodeID.Value)
            End If

            Dim vBusiness As New PostcodeBusinessLogic()
            Dim obj As New CsPostCodePrice()

            If currentId > 0 Then
                obj = vBusiness.GetPostcodeById(currentId)
            End If
            If IsNothing(obj) Then
                obj = New CsPostCodePrice()
            End If

            With obj
                .ID = currentId

                Dim checkPostcode As CsPostCodePrice = vBusiness.FindPostcode(txtPostcode.Text.Trim())
                If Not IsNothing(checkPostcode) AndAlso checkPostcode.ID > 0 AndAlso checkPostcode.ID <> currentId Then
                    phMessage.Visible = True
                    ltrMessage.Text = "[" & txtPostcode.Text & "] " & "already existed."
                    Exit Sub
                Else
                    phMessage.Visible = False
                    ltrMessage.Text = ""
                End If

                .PostCode = txtPostcode.Text

                If Not String.IsNullOrWhiteSpace(Price.Text.Trim()) Then
                    .Price = CDec(Price.Text.Trim())
                End If
                If Not String.IsNullOrWhiteSpace(MinimumOrderValue.Text.Trim()) Then
                    .MinOrder = CDec(MinimumOrderValue.Text.Trim())
                End If

                .AllowDelivery = AllowDelivery.Checked
                Try
                    If vBusiness.SavePostCodePrice(obj) Then
                        LoadAllPostcode()
                        ltrMessage.Text = AdminConstanst.ChangesHasBeenSaved
                        phMessage.Visible = True
                    End If
                Catch ex As Exception
                    ltrMessage.Text = AdminConstanst.CouldNotSaveTheChanges
                    phMessage.Visible = True
                    _log.Error("Message: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try

            End With

            HidePostcodeEditorModal()
        End Sub

        Private Sub ShowPostcodeEditorModal(Optional vId As Integer = 0)
            If vId > 0 Then
                LoadPostcode(vId)
            End If

            Dim sbJsScript As New StringBuilder()
            With sbJsScript
                .AppendLine("<script type='text/javascript'>")
                .AppendLine("    $('#postcodeEditorModal').modal('show');")
                .AppendLine("</script>")
            End With

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ShowPostcodeEditorModalScript", sbJsScript.ToString(), False)
        End Sub

        Private Sub HidePostcodeEditorModal()
            Dim sbJsScript As New StringBuilder()
            With sbJsScript
                .AppendLine("<script type='text/javascript'>")
                .AppendLine("    $('#postcodeEditorModal').modal('hide');")
                .AppendLine("</script>")
            End With

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "HidePostcodeEditorModalScript", sbJsScript.ToString(), False)
        End Sub

        Protected Sub ShowMessageBox(ByVal message As String)
            Dim script As String = ""
            script = String.Format("alert('{0}');", message)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", script, True)
        End Sub
    End Class
End Namespace