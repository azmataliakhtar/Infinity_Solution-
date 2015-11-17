Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Runtime.InteropServices.ComTypes
Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports System.IO
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Logging.Log4Net
Imports Org.BouncyCastle.Asn1.X509.Qualified

Namespace Ajax

    <System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    <ScriptService> _
    Public Class ShoppingService
        Inherits System.Web.Services.WebService

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        <WebMethod(), ScriptMethod()> _
        Public Function SayHello(contextKey As String) As String
            Return "Hello: " & contextKey
        End Function

        <WebMethod(), ScriptMethod()> _
        Public Function GetDealDetailById(contextKey As String) As String
            _log.Info("BEGIN")
            Try
                If String.IsNullOrEmpty(contextKey) Then
                    _log.Info("END")
                    Return ""
                End If

                If Not IsNumeric(contextKey) Then
                    _log.Info("END")
                    Return ""
                End If

                Dim bzMenu As New MenuBusinessLogic(GetDbConnectionString())
                Dim detail As CsDealDetail = Nothing
                Try
                    detail = bzMenu.GetDealDetailById(CInt(contextKey), True)
                Catch ex As Exception
                    _log.Error(ex)
                End Try

                If Not IsNothing(detail) AndAlso detail.ID > 0 Then
                    'Me.SelectedDeal = detail

                    Dim sb As New StringBuilder()
                    Using strWriter As New StringWriter(sb)
                        Using writer As New HtmlTextWriter(strWriter)

                            Dim div As New HtmlGenericControl("div")
                            With div
                                .ID = "div_deal_options"
                            End With

                            'Dim divMealName As New HtmlGenericControl("div")
                            'With divMealName
                            '    .ID = "div_deal_name"
                            '    .InnerHtml = "<span style=""font-size: 20px;font-weight: normal;"">" & detail.Name & "</span>"
                            'End With
                            'div.Controls.Add(divMealName)

                            '*******************************************************
                            ' Your current selection for the deals
                            '*******************************************************
                            'If Not IsNothing(Me.SelectedDeal) Then
                            '    Dim divCurrentSelection As New HtmlGenericControl("div")
                            '    With divCurrentSelection
                            '        .ID = "div_current_selection"
                            '    End With

                            '    Dim selectionStr As String = ""

                            '    If Not IsNothing(Me.SelectedToppingList) Then
                            '        Dim selectedToppingStr As String = (From tp In Me.SelectedToppingList Where Not IsNothing(tp) Select tp).Aggregate("", Function(current, tp) current & "," & tp.Name)
                            '        If Not String.IsNullOrEmpty(selectedToppingStr) Then
                            '            'Dim divToppingSelection As New HtmlGenericControl("div")
                            '            'With divToppingSelection
                            '            '    .ID = "div_topping_selection"
                            '            '    .InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;With Topping:&nbsp;<span style=""font-size: 12px;font-weight: normal;"">" & selectedToppingStr & "</span>"
                            '            'End With

                            '            'divCurrentSelection.Controls.Add(divToppingSelection)

                            '            selectionStr = selectionStr & selectedToppingStr
                            '        End If
                            '    End If

                            '    If Not IsNothing(Me.SelectedDressingList) Then
                            '        Dim dressingSelection As String = (From t In Me.SelectedDressingList Where Not IsNothing(t) Select t).Aggregate("", Function(current, t) current & "," & t.Name)
                            '        If Not String.IsNullOrEmpty(dressingSelection) Then
                            '            'Dim divDressingSelection As New HtmlGenericControl("div")
                            '            'With divDressingSelection
                            '            '    .ID = "div_topping_selection"
                            '            '    .InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;With Dressing:&nbsp;<span style=""font-size: 12px;font-weight: normal;"">" & dressingSelection & "</span>"
                            '            'End With

                            '            'divCurrentSelection.Controls.Add(divDressingSelection)

                            '            selectionStr = selectionStr & dressingSelection
                            '        End If
                            '    End If

                            '    If Not IsNothing(Me.SelectedOptionList) Then
                            '        Dim optionSeletion As String = (From t In Me.SelectedOptionList Where Not IsNothing(t) Select t).Aggregate("", Function(current, t) current & "," & t.Name)
                            '        If Not String.IsNullOrEmpty(optionSeletion) Then
                            '            'Dim divOptionSelection As New HtmlGenericControl("div")
                            '            'With divOptionSelection
                            '            '    .ID = "div_topping_selection"
                            '            '    .InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;With Dressing:&nbsp;<span style=""font-size: 12px;font-weight: normal;"">" & optionSeletion & "</span>"
                            '            'End With

                            '            'divCurrentSelection.Controls.Add(divOptionSelection)

                            '            selectionStr = selectionStr & optionSeletion
                            '        End If
                            '    End If

                            '    If Not IsNothing(Me.SelectedMenu) Then
                            '        Dim divMenu As New HtmlGenericControl("div")
                            '        With divMenu
                            '            .ID = "div_deal_name"
                            '            If String.IsNullOrEmpty(selectionStr) Then
                            '                .InnerHtml = "&nbsp;&nbsp;-&nbsp;<span style=""font-size: 16px;font-weight: normal;"">" & Me.SelectedMenu.Name & "</span>"
                            '            Else
                            '                .InnerHtml = "&nbsp;&nbsp;-&nbsp;<span style=""font-size: 16px;font-weight: normal;"">" & Me.SelectedMenu.Name & " (" & selectionStr.Trim(",") & ") " & "</span>"
                            '            End If

                            '        End With
                            '        divCurrentSelection.Controls.Add(divMenu)
                            '    End If

                            '    div.Controls.Add(divCurrentSelection)
                            'End If
                            '*******************************************************

                            'Dim divHeader As New HtmlGenericControl("div")
                            'With divHeader
                            '    .ID = "menu_extra_header"
                            '    .InnerHtml = "<p>Please choose your meal options:</p>"
                            'End With
                            'div.Controls.Add(divHeader)

                            If Not IsNothing(detail.LinkedMenuList1) AndAlso detail.LinkedMenuList1.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "-- Please choose meal option --"})
                                items.AddRange(detail.LinkedMenuList1)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_1"
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                End With

                                div.Controls.Add(ddl)
                            End If

                            If Not IsNothing(detail.LinkedMenuList2) AndAlso detail.LinkedMenuList2.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "-- Please choose meal option --"})
                                items.AddRange(detail.LinkedMenuList2)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_2"
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                End With

                                div.Controls.Add(ddl)
                            End If

                            If Not IsNothing(detail.LinkedMenuList3) AndAlso detail.LinkedMenuList3.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "-- Please choose meal option --"})
                                items.AddRange(detail.LinkedMenuList3)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_3"
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                End With

                                div.Controls.Add(ddl)
                            End If

                            If Not IsNothing(detail.LinkedMenuList4) AndAlso detail.LinkedMenuList4.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "-- Please choose meal option --"})
                                items.AddRange(detail.LinkedMenuList4)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_4"
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                End With

                                div.Controls.Add(ddl)
                            End If

                            If Not IsNothing(detail.LinkedMenuList5) AndAlso detail.LinkedMenuList5.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "--Please choose meal option--"})
                                items.AddRange(detail.LinkedMenuList5)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_5"
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                End With

                                div.Controls.Add(ddl)
                            End If

                            If Not IsNothing(detail.LinkedMenuList6) AndAlso detail.LinkedMenuList6.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "--Please choose meal option--"})
                                items.AddRange(detail.LinkedMenuList6)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_6"
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                End With

                                div.Controls.Add(ddl)
                            End If

                            If Not IsNothing(detail.LinkedMenuList7) AndAlso detail.LinkedMenuList7.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "--Please choose meal option--"})
                                items.AddRange(detail.LinkedMenuList7)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_7"
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                End With

                                div.Controls.Add(ddl)
                            End If

                            If Not IsNothing(detail.LinkedMenuList8) AndAlso detail.LinkedMenuList8.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "--Please choose meal option--"})
                                items.AddRange(detail.LinkedMenuList8)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_8"
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                End With

                                div.Controls.Add(ddl)
                            End If

                            If Not IsNothing(detail.LinkedMenuList9) AndAlso detail.LinkedMenuList9.Count > 0 Then

                                Dim items As New List(Of CsMenuItem)
                                items.Add(New CsMenuItem() With {.ID = -1, .Name = "--Please choose meal option--"})
                                items.AddRange(detail.LinkedMenuList9)

                                Dim ddl As New DropDownList
                                With ddl
                                    .ID = "ddl_options_9"
                                    .DataValueField = "ID"
                                    .DataTextField = "Name"
                                    .DataSource = items
                                    .DataBind()
                                    .CssClass = "deals-details-item-options normal-input"
                                    .Attributes.Add("onchange", "ShowToppingOrDressingOnSeletionOfOption(this)")
                                End With

                                div.Controls.Add(ddl)
                            End If

                            div.RenderControl(writer)
                        End Using
                    End Using

                    Return sb.ToString()
                End If

            Catch ex As Exception
                _log.Error(ex)
            End Try

            _log.Info("END")
            Return String.Empty
        End Function


        <WebMethod()> _
        <ScriptMethod()> _
        Public Function GetExtraItems(contextKey As String)
            'Argument "contextKey"" should be in format type=topping/dressing/options&menu_id=Menu Item ID&option_cat_id=Option Category ID
            _log.Info("BEGIN")
            Try
                If String.IsNullOrEmpty(contextKey) Then
                    _log.Info("END")
                    Return "Opps! There is an error happned. Please contact your supporter!"
                End If

                'If Not IsNumeric(contextKey) Then
                '    Return ""
                'End If

                'Extract argument values
                Dim args() As String = contextKey.Split("&")
                Dim typeStr As String = ""
                Dim menuId As Integer = 0
                Dim optionCatId As Integer = 0

                Dim dealId As Integer = 0

                If args.Length = 3 Then
                    Dim typeNameValue() As String = args(0).Split("=")
                    If typeNameValue.Length = 2 AndAlso typeNameValue(0).ToLower() = "type" Then
                        typeStr = typeNameValue(1).ToLower()
                    End If

                    Dim menuIdNameValue() As String = args(1).Split("=")
                    If menuIdNameValue.Length = 2 AndAlso menuIdNameValue(0).ToLower() = "menu_id" Then
                        If IsNumeric(menuIdNameValue(1)) Then
                            menuId = CInt(menuIdNameValue(1))
                        End If
                    End If

                    Dim dealIdNameValue() As String = args(2).Split("=")
                    If dealIdNameValue.Length = 2 AndAlso dealIdNameValue(0).ToLower() = "deal_id" Then
                        If IsNumeric(dealIdNameValue(1)) Then
                            dealId = CInt(dealIdNameValue(1))
                        End If
                    End If

                ElseIf args.Length = 4 Then
                    Dim typeNameValue() As String = args(0).Split("=")
                    If typeNameValue.Length = 2 AndAlso typeNameValue(0).ToLower() = "type" Then
                        typeStr = typeNameValue(1).ToLower()
                    End If

                    Dim menuIdNameValue() As String = args(1).Split("=")
                    If menuIdNameValue.Length = 2 AndAlso menuIdNameValue(0).ToLower() = "menu_id" Then
                        If IsNumeric(menuIdNameValue(1)) Then
                            menuId = CInt(menuIdNameValue(1))
                        End If
                    End If

                    Dim optionCatIdNameValue() As String = args(2).Split("=")
                    If optionCatIdNameValue.Length = 2 AndAlso optionCatIdNameValue(0).ToLower() = "option_cat_id" Then
                        If IsNumeric(optionCatIdNameValue(1)) Then
                            optionCatId = CInt(optionCatIdNameValue(1))
                        End If
                    End If

                    Dim dealIdNameValue() As String = args(3).Split("=")
                    If dealIdNameValue.Length = 2 AndAlso dealIdNameValue(0).ToLower() = "deal_id" Then
                        If IsNumeric(dealIdNameValue(1)) Then
                            dealId = CInt(dealIdNameValue(1))
                        End If
                    End If

                End If

                If typeStr = "" OrElse Not (typeStr = "topping" OrElse typeStr = "dressing" OrElse typeStr = "options") OrElse menuId = 0 Then
                    Return ""
                End If

                Dim sb As New StringBuilder()

                Dim bzMenu As New MenuBusinessLogic(GetDbConnectionString())
                Dim item As CsMenuItem = Nothing
                Dim detail As CsDealDetail = Nothing
                Try
                    detail = bzMenu.GetDealDetailById(dealId, False)
                    item = bzMenu.GetMenuItem(menuId)
                Catch ex As Exception
                    _log.Error(ex)
                End Try

                Using strWriter As New StringWriter(sb)
                    Using writer As New HtmlTextWriter(strWriter)

                        Dim div As New HtmlGenericControl("div")
                        With div
                            .ID = "div_toppings"
                        End With

                        Dim divDealName As New HtmlGenericControl("div")
                        With divDealName
                            .ID = "div_deal_name"
                            .InnerHtml = "<span style=""font-size: 20px;font-weight: normal;"">" & detail.Name & "</span>"
                        End With
                        div.Controls.Add(divDealName)

                        Dim divMealName As New HtmlGenericControl("div")
                        With divMealName
                            .ID = "div_deal_name"
                            .InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;<span style=""font-size: 16px;font-weight: normal;"">" & item.Name & "</span>"
                        End With
                        div.Controls.Add(divMealName)

                        'Dim h3 As New HtmlGenericControl("h3")
                        'With h3
                        '    .ID = "h_menu_item_name"
                        '    .InnerText = "" & item.Name & ""
                        'End With
                        'div.Controls.Add(h3)

                        Dim divHeader As New HtmlGenericControl("div")
                        With divHeader
                            .ID = "menu_extra_header"
                            .InnerHtml = "<p>Please choose the following extra as your toppings:</p>"
                        End With
                        div.Controls.Add(divHeader)

                        If typeStr = "topping" Then
                            BuildToppingHtmlOutput(div)
                        ElseIf typeStr = "dressing" Then
                            BuildDressingHtmlOutput(div)
                        ElseIf typeStr = "options" Then
                            BuildOptionsHtmlOutput(optionCatId, div)
                        End If
                        div.RenderControl(writer)
                    End Using
                End Using

                Return sb.ToString()
            Catch ex As Exception
                _log.Error(ex)
            End Try

            _log.Info("END")
            Return String.Empty
        End Function

        <WebMethod(), ScriptMethod()> _
        Public Function GetMenuLinkingInfo(menuId As String, dealId As String)
            _log.Info("BEGIN")

            Try
                If String.IsNullOrWhiteSpace(menuId) OrElse Not IsNumeric(menuId) Then
                    _log.Info("END")
                    Return ""
                End If

                Dim menuIdInt As Integer = CInt(menuId)
                Dim bzMenu As New MenuBusinessLogic(GetDbConnectionString())
                Dim item As CsMenuItem = Nothing
                Try
                    item = bzMenu.GetMenuItem(menuIdInt)
                Catch ex As Exception
                    _log.Error(ex)
                End Try

                If Not IsNothing(item) Then
                    If item.HasTopping Then
                        Return "type=topping&menu_id=" & item.ID.ToString() & "&deal_id=" & dealId
                    End If

                    If item.HasDressing Then
                        Return "type=dressing&menu_id=" & item.ID.ToString() & "&deal_id=" & dealId
                    End If

                    If item.OptionId1 > 0 Then
                        Return "type=options&menu_id=" & item.ID.ToString() & "&option_cat_id=" & item.OptionId1.ToString() & "&deal_id=" & dealId
                    End If

                    Return "no_extra_items"
                End If
            Catch ex As Exception
                _log.Error(ex)
                Return String.Empty
            End Try

            _log.Info("END")
            Return ""
        End Function

        <WebMethod(), ScriptMethod()> _
        Public Function GetSecondMenuLinkingInfo(menuId As String, dealId As String)
            _log.Info("BEGIN")

            Try
                If String.IsNullOrWhiteSpace(menuId) OrElse Not IsNumeric(menuId) Then
                    _log.Info("END")
                    Return ""
                End If

                Dim menuIdInt As Integer = CInt(menuId)
                Dim bzMenu As New MenuBusinessLogic(GetDbConnectionString())
                Dim item As CsMenuItem = Nothing
                Try
                    item = bzMenu.GetMenuItem(menuIdInt)
                Catch ex As Exception
                    _log.Error(ex)
                End Try

                If Not IsNothing(item) Then
                    If item.OptionId2 > 0 Then
                        _log.Info("END")
                        Return "type=options&menu_id=" & item.ID.ToString() & "&option_cat_id=" & item.OptionId2.ToString() & "&deal_id=" & dealId
                    End If

                    _log.Info("END")
                    Return "no_second_extra_items"
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try

            _log.Info("END")
            Return ""
        End Function

        <WebMethod(), ScriptMethod()> _
        Public Function UpdateSelectionForDeal(menuId As String, toppingIds As String, dressingIds As String, optionIds As String, selector As String) As String
            _log.Info("BEGIN")

            Dim sb As New StringBuilder()
            Dim bzMenu As New MenuBusinessLogic(GetDbConnectionString())

            Try
                Using strWriter As New StringWriter(sb)
                    Using writer As New HtmlTextWriter(strWriter)

                        Dim divCurrentSelection As New HtmlGenericControl("div")
                        With divCurrentSelection
                            .ID = "div_current_selection_" & selector
                        End With

                        Dim selectedMenu As CsMenuItem = Nothing
                        selectedMenu = bzMenu.GetMenuItem(menuId)

                        Dim arrToppingId() As Integer = (From t In toppingIds.Split(";"c)
                                                         Where Not String.IsNullOrEmpty(t) AndAlso IsNumeric(t)
                                                         Select CInt(t)).ToArray()

                        Dim arrDressingId() As Integer = (From t In dressingIds.Split(";"c)
                                                          Where Not String.IsNullOrEmpty(t) AndAlso IsNumeric(t)
                                                          Select CInt(t)).ToArray()

                        Dim arrOptionId() As Integer = (From t In optionIds.Split(";"c)
                                                        Where Not String.IsNullOrEmpty(t) AndAlso IsNumeric(t)
                                                        Select CInt(t)).ToArray()


                        Dim selectedToppingList As List(Of CsMenuTopping) = Nothing
                        If Not IsNothing(arrToppingId) AndAlso arrToppingId.Length > 0 Then
                            Dim bzTopping As New BzMenuTopping(GetDbConnectionString())
                            Dim allTopping As IQueryable(Of CsMenuTopping) = bzTopping.GetAllMenuToppings().AsQueryable()
                            selectedToppingList = (From tp In allTopping Where arrToppingId.Contains(tp.ID) Select tp).ToList()
                        End If

                        Dim selectedDressingList As List(Of CsMenuDressing) = Nothing
                        If Not IsNothing(arrDressingId) AndAlso arrDressingId.Length > 0 Then
                            Dim allDressing As IQueryable(Of CsMenuDressing) = bzMenu.GetAllMenuDressings().AsQueryable()
                            selectedDressingList = (From tp In allDressing Where arrDressingId.Contains(tp.ID) Select tp).ToList()
                        End If

                        Dim selectedOptionList As List(Of CsOptionDetail) = Nothing
                        If Not IsNothing(arrOptionId) AndAlso arrOptionId.Length > 0 Then
                            Dim optionList As List(Of CsOptionDetail) = New List(Of CsOptionDetail)()
                            For index As Integer = 0 To arrOptionId.Length - 1
                                Dim opt As CsOptionDetail = bzMenu.GetOptionDetailByID(arrOptionId(index))
                                If Not IsNothing(opt) Then
                                    optionList.Add(opt)
                                End If
                            Next

                            selectedOptionList = optionList
                        End If

                        '*******************************************************
                        ' Your current selection for the deals
                        '*******************************************************
                        Dim selectionStr As String = ""

                        If Not IsNothing(selectedToppingList) Then
                            Dim selectedToppingStr As String = (From tp In selectedToppingList Where Not IsNothing(tp) Select tp).Aggregate("", Function(current, tp) current & ", " & tp.Name)
                            If Not String.IsNullOrEmpty(selectedToppingStr) Then
                                selectionStr = selectionStr & selectedToppingStr
                            End If
                        End If

                        If Not IsNothing(selectedDressingList) Then
                            Dim dressingSelection As String = (From t In selectedDressingList Where Not IsNothing(t) Select t).Aggregate("", Function(current, t) current & ", " & t.Name)
                            If Not String.IsNullOrEmpty(dressingSelection) Then
                                selectionStr = selectionStr & dressingSelection
                            End If
                        End If

                        If Not IsNothing(selectedOptionList) Then
                            Dim optionSeletion As String = (From t In selectedOptionList Where Not IsNothing(t) Select t).Aggregate("", Function(current, t) current & ", " & t.Name)
                            If Not String.IsNullOrEmpty(optionSeletion) Then
                                selectionStr = selectionStr & optionSeletion
                            End If
                        End If

                        If Not IsNothing(selectedMenu) Then
                            If String.IsNullOrEmpty(selectionStr) Then
                                divCurrentSelection.InnerHtml = "&nbsp;&nbsp;-&nbsp;<span style=""font-size: 16px;font-weight: normal;"">" & selectedMenu.Name & "</span>"
                            Else
                                divCurrentSelection.InnerHtml = "&nbsp;&nbsp;-&nbsp;<span style=""font-size: 16px;font-weight: normal;"">" & selectedMenu.Name & " (" & selectionStr.Trim().Trim(",") & ") " & "</span>"
                            End If
                        End If
                        '*******************************************************
                        divCurrentSelection.RenderControl(writer)
                    End Using
                End Using

            Catch ex As Exception
                _log.Error(ex)
            End Try

            _log.Info("END")
            Return sb.ToString()
        End Function

        Private Sub BuildToppingHtmlOutput(ByRef div As HtmlGenericControl)
            _log.Info("BEGIN")

            Try
                Dim bzTopping As New BzMenuTopping(GetDbConnectionString())
                Dim toppings As IQueryable(Of CsMenuTopping) = bzTopping.GetAllMenuToppings().AsQueryable()

                If Not IsNothing(toppings) AndAlso toppings.Any() Then
                    Dim table As New HtmlTable
                    table.Style.Add("width", "100%")


                    'Dim headerRow As New HtmlTableRow
                    'Dim headerCell As New HtmlTableCell
                    'headerCell.InnerHtml = "<b>Topping</b>"
                    'headerCell.Style.Add("width", "70%")
                    'headerCell.Style.Add("padding", "5px 5px")
                    'headerCell.Style.Add("background", "#ACACAC")
                    'headerCell.ColSpan = 2
                    'headerRow.Cells.Add(headerCell)

                    'table.Rows.Add(headerRow)

                    Dim toppingList As List(Of CsMenuTopping) = toppings.ToList()

                    For index As Integer = 0 To toppingList.Count - 1 Step 2
                        Dim tpp As CsMenuTopping = toppingList(index)

                        Dim row As New HtmlTableRow
                        row.Attributes.Add("class", "on_off")

                        Dim cell As New HtmlTableCell
                        cell.InnerHtml = "<input type=""checkbox"" id=""on_off_" & tpp.ID & """ class=""chk_on_off"" value=""yes"" name=""on_off_"" onchange=""MarkToppingToAdd(this," & tpp.ID & ")"" /><label for=""on_off_" & tpp.ID & """ >" & tpp.Name & "</label>"
                        cell.Style.Add("padding", "0px 5px")
                        row.Cells.Add(cell)

                        If index + 1 < toppingList.Count Then
                            Dim tpp2 As CsMenuTopping = toppingList(index + 1)
                            Dim cell2 As HtmlTableCell = New HtmlTableCell
                            cell2.InnerHtml = "<input type=""checkbox"" id=""on_off_" & tpp2.ID & """ class=""chk_on_off"" value=""yes"" name=""on_off_"" onchange=""MarkToppingToAdd(this," & tpp2.ID & ")"" /><label for=""on_off_" & tpp2.ID & """>" & tpp2.Name & "</label>"
                            cell2.Style.Add("padding", "0px 5px")
                            row.Cells.Add(cell2)
                        Else
                            Dim cell2 As HtmlTableCell = New HtmlTableCell
                            cell2.InnerHtml = ""
                            cell2.Style.Add("padding", "0px 5px")
                            row.Cells.Add(cell2)
                        End If

                        table.Rows.Add(row)
                    Next

                    div.Controls.Add(table)
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try
            _log.Info("END")
        End Sub

        Private Sub BuildDressingHtmlOutput(ByRef div As HtmlGenericControl)
            _log.Info("BEGIN")
            Try
                Dim bzMenu As New MenuBusinessLogic(GetDbConnectionString())
                Dim dressings As IQueryable(Of CsMenuDressing) = bzMenu.GetAllMenuDressings().AsQueryable()

                If Not IsNothing(dressings) AndAlso dressings.Any() Then
                    Dim table As New HtmlTable
                    table.Style.Add("width", "100%")

                    Dim headerRow As New HtmlTableRow
                    Dim headerCell As New HtmlTableCell
                    headerCell.InnerHtml = "<b>Dressing</b>"
                    headerCell.Style.Add("width", "70%")
                    headerCell.Style.Add("padding", "5px 5px")
                    headerCell.Style.Add("background", "#ACACAC")
                    headerCell.ColSpan = 2
                    headerRow.Cells.Add(headerCell)

                    table.Rows.Add(headerRow)

                    Dim dressingList As List(Of CsMenuDressing) = dressings.ToList()
                    For index As Integer = 0 To dressings.Count - 1 Step 2
                        Dim drs As CsMenuDressing = dressingList(index)

                        Dim row As New HtmlTableRow
                        row.Attributes.Add("class", "on_off")

                        Dim cell As New HtmlTableCell
                        cell.InnerHtml = "<input type=""checkbox"" id=""on_off_" & drs.ID & """ class=""chk_on_off"" value=""yes"" name=""on_off_"" onchange=""MarkDressingToAdd(this," & drs.ID & ")"" /><label for=""on_off_" & drs.ID & """ >" & drs.Name & "</label>"
                        cell.Style.Add("padding", "0px 5px")
                        row.Cells.Add(cell)

                        If index + 1 < dressingList.Count Then
                            Dim drs2 As CsMenuDressing = dressingList(index + 1)
                            Dim cell2 As HtmlTableCell = New HtmlTableCell
                            cell2.InnerHtml = "<input type=""checkbox"" id=""on_off_" & drs2.ID & """ class=""chk_on_off"" value=""yes"" name=""on_off_"" onchange=""MarkDressingToAdd(this," & drs2.ID & ")"" /><label for=""on_off_" & drs2.ID & """ >" & drs2.Name & "</label>"
                            cell2.Style.Add("padding", "0px 5px")
                            row.Cells.Add(cell2)
                        Else
                            Dim cell2 As HtmlTableCell = New HtmlTableCell
                            cell2.InnerHtml = ""
                            cell2.Style.Add("padding", "0px 5px")
                            row.Cells.Add(cell2)
                        End If

                        table.Rows.Add(row)
                    Next

                    div.Controls.Add(table)
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try
            _log.Info("END")
        End Sub

        Private Sub BuildOptionsHtmlOutput(ByVal optionCatId As Integer, ByRef div As HtmlGenericControl)
            _log.Info("BEGIN")
            Try
                Dim bzMenu As New MenuBusinessLogic(GetDbConnectionString())
                Dim options As IQueryable(Of CsOptionDetail) = bzMenu.GetOptionDetails(optionCatId).AsQueryable()

                If Not IsNothing(options) AndAlso options.Any() Then
                    Dim table As New HtmlTable
                    table.Style.Add("width", "100%")

                    Dim headerRow As New HtmlTableRow
                    Dim headerCell As New HtmlTableCell
                    headerCell.InnerHtml = "<b>Options</b>"
                    headerCell.Style.Add("width", "70%")
                    headerCell.Style.Add("padding", "3px 5px")
                    headerCell.Style.Add("background", "#ACACAC")
                    headerRow.Cells.Add(headerCell)

                    headerCell = New HtmlTableCell()
                    headerCell.InnerHtml = "<b>&nbsp;</b>"
                    headerCell.Style.Add("width", "30%")
                    headerCell.Style.Add("padding", "3px 5px")
                    headerCell.Style.Add("background", "#ACACAC")
                    headerRow.Cells.Add(headerCell)

                    table.Rows.Add(headerRow)

                    For Each opt As CsOptionDetail In options
                        Dim row As New HtmlTableRow
                        row.Attributes.Add("class", "on_off")

                        Dim cell As New HtmlTableCell
                        cell.InnerHtml = "<input type=""checkbox"" id=""on_off_" & opt.ID & """ class=""chk_on_off"" value=""yes"" name=""on_off_""  onchange=""MarkOptionToAdd(this," & opt.ID & ")""/><label for=""on_off_" & opt.ID & """ >" & opt.Name & "</label>"
                        cell.Style.Add("padding", "0px 5px")
                        row.Cells.Add(cell)

                        cell = New HtmlTableCell
                        cell.InnerHtml = "<span>" & FormatCurrency(opt.UnitPrice, 2) & "</span>"
                        cell.Style.Add("padding", "0px 5px")
                        row.Cells.Add(cell)

                        table.Rows.Add(row)
                    Next

                    div.Controls.Add(table)
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try
            _log.Info("END")
        End Sub

        Private Function GetDbConnectionString() As String
            _log.Info("BEGIN")
            Dim connectionStr As String = ConfigurationManager.ConnectionStrings("PizzaWebConnectionString").ConnectionString
            _log.Info("END")
            Return connectionStr
        End Function

#Region "Your Selection for Deals"


#End Region

    End Class
End Namespace