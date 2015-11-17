Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports INF.Web.UI.Logging.Log4Net

Namespace Admin
    Public Class MenuItem
        Inherits AdminPage

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected ReadOnly Property CategoryID As Integer
            Get
                Dim catId As Integer = 0
                If (Not String.IsNullOrWhiteSpace(Page.Request.QueryString("CategoryID"))) AndAlso IsNumeric(Page.Request.QueryString("CategoryID")) Then
                    catId = CInt(Page.Request.QueryString("CategoryID"))
                End If
                Return catId
            End Get
        End Property

        Protected ReadOnly Property MenuItemID As Integer
            Get
                Dim itemId As Integer = 0
                If (Not String.IsNullOrWhiteSpace(Page.Request.QueryString("MenuItemId"))) AndAlso IsNumeric(Page.Request.QueryString("MenuItemId")) Then
                    itemId = CInt(Page.Request.QueryString("MenuItemId"))
                End If
                Return itemId
            End Get
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                If CategoryID > 0 Then
                    LoadCategory(CategoryID)
                    ddlCategories.Visible = False
                    txtCategoryName.Visible = True
                Else
                    LoadAllCategories()
                    ddlCategories.Visible = True
                    txtCategoryName.Visible = False
                End If

                LoadMenuItems(CategoryID)
            End If
        End Sub

        Protected Sub btnNewMenuItem_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnNewMenuItem.Click
            ltrMenuItemEditorHeaderText.Text = "Add New Menu Item"

            txtMenuItemName.Text = ""
            txtItemPosition.Text = 0
            txtPreparationTime.Text = 0
            chkActive.Checked = False
            chkBaseSelection.Checked = False
            txtToppingPrice1.Text = FormatNumber(0, 2)
            txtToppingPrice2.Text = FormatNumber(0, 2)
            txtToppingPrice3.Text = FormatNumber(0, 2)
            txtDeliveryPrice.Text = FormatNumber(0, 2)
            txtCollectionPrice.Text = FormatNumber(0, 2)

            chkLinkDressingOrTopping.Checked = False
            chkLinkMenu.Checked = False
            ddlMenuLink1.SelectedIndex = 0
            ddlMenuLink2.SelectedIndex = 0

            PromotionText.Text = ""
            Remarks.Text = ""
            hdnMenuItemID.Value = 0

            ShowMenuItemEditorModal()
        End Sub

        Private Sub gvMenuItem_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvMenuItem.RowCommand
            Dim index = -1
            If Not IsNothing(e.CommandArgument) AndAlso IsNumeric(e.CommandArgument) Then
                index = CInt(e.CommandArgument)
            End If

            If index < 0 Then
                Exit Sub
            End If

            Dim itemId = CInt(gvMenuItem.DataKeys(index).Value)
            Dim bizMenu = New MenuBusinessLogic()

            Try
                'Dim menuItem = bizMenu.GetMenuItem(itemId)
                Select Case e.CommandName
                    Case "SelectMenuItem"
                        ltrMenuItemEditorHeaderText.Text = "Edit Existing Menu Item"
                        ShowMenuItemEditorModal(itemId)
                        Exit Select
                    Case "DeleteMenuItem"
                        Dim itemToDelete = bizMenu.GetMenuItem(itemId)
                        If Not IsNothing(itemToDelete) Then
                            bizMenu.DeleteMenuItem(itemToDelete.ID)
                            LoadMenuItems(CategoryID)

                            ShowMessageBox("[" & itemToDelete.Name & "] " & "has been deleted successfully.")
                        Else
                            ShowMessageBox("[" & itemToDelete.Name & "] " & "could not be found!")
                        End If
                        Exit Select

                    Case "SubMenuItems"
                        Dim itemToCheck = bizMenu.GetMenuItem(itemId)
                        If Not IsNothing(itemToCheck) AndAlso itemToCheck.ID > 0 Then
                            Response.RedirectTo("SubMenuItem.aspx?MenuId=" & itemToCheck.ID)
                        End If
                        Exit Select
                    Case Else
                        Exit Select
                End Select
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Sub LoadAllCategories()
            Try
                Dim vBusiness As New MenuBusinessLogic()
                Dim categories As IEnumerable(Of CsMenuCategory) = vBusiness.GetAllMenuCategories()
                If Not IsNothing(categories) Then

                    Dim categoryList As List(Of CsMenuCategory) = categories.ToList()
                    categoryList.Insert(0, New CsMenuCategory() With {.ID = 0, .Name = "-- Select Category --"})

                    ddlCategories.DataTextField = "Name"
                    ddlCategories.DataValueField = "ID"
                    ddlCategories.DataSource = categoryList
                    ddlCategories.DataBind()

                    ddlCategories.SelectedIndex = 0
                End If

            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Sub LoadCategory(vCategoryId As Integer)
            If vCategoryId = 0 Then
                Exit Sub
            End If

            Try
                Dim vBusiness As New MenuBusinessLogic()
                Dim currentCategory = vBusiness.GetMenuCategoryByID(vCategoryId)
                If Not IsNothing(currentCategory) Then
                    hdnCategoryID.Value = currentCategory.ID
                    txtCategoryName.Text = currentCategory.Name
                End If

            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Protected Sub SaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
            If Not Page.IsValid Then
                Return
            End If

            Dim vBusiness As New MenuBusinessLogic()
            Dim vMenuItem As New CsMenuItem()
            Dim menuId As Integer = 0
            If (Not String.IsNullOrWhiteSpace(hdnMenuItemID.Value)) AndAlso IsNumeric(hdnMenuItemID.Value) Then
                menuId = CDec(hdnMenuItemID.Value)
            Else
                menuId = 0
            End If

            If menuId > 0 Then
                vMenuItem = vBusiness.GetMenuItem(menuId)
                If IsNothing(vMenuItem) Then
                    vMenuItem = New CsMenuItem()
                End If
            End If

            With vMenuItem
                If Not String.IsNullOrEmpty(hdnCategoryID.Value) AndAlso IsNumeric(hdnCategoryID.Value) Then
                    .CategoryID = CInt(hdnCategoryID.Value)
                End If

                If ddlCategories.Enabled Then
                    If Not IsNothing(ddlCategories.SelectedValue) AndAlso IsNumeric(ddlCategories.SelectedValue) Then
                        .CategoryID = CInt(ddlCategories.SelectedValue)
                    End If
                End If

                If .CategoryID = 0 Then
                    ShowMessageBox("[Category] must be selected.")
                    Exit Sub
                End If

                Dim existedItemWithTheSameName = vBusiness.GetMenuItemByName(.CategoryID, txtMenuItemName.Text.Trim(), True)
                If Not IsNothing(existedItemWithTheSameName) AndAlso existedItemWithTheSameName.ID <> .ID AndAlso existedItemWithTheSameName.ID > 0 Then
                    ShowMessageBox("There is already an item with the same name. Please enter different name.")
                    Return
                End If

                .Name = txtMenuItemName.Text.Trim()

                If (Not String.IsNullOrWhiteSpace(txtItemPosition.Text)) AndAlso IsNumeric(txtItemPosition.Text) Then
                    .ItemPosition = CInt(txtItemPosition.Text.Trim())
                Else
                    .ItemPosition = 0
                End If
                If Not String.IsNullOrEmpty(txtPreparationTime.Text) AndAlso IsNumeric(txtPreparationTime.Text) Then
                    .PreparationTime = CInt(txtPreparationTime.Text.Trim())
                Else
                    .PreparationTime = 0
                End If

                .IsActive = chkActive.Checked
                .HasBase = chkBaseSelection.Checked

                If (Not String.IsNullOrWhiteSpace(txtToppingPrice1.Text)) AndAlso IsNumeric(txtToppingPrice1.Text) Then
                    .ToppingPrice1 = CDec(txtToppingPrice1.Text.Trim())
                End If
                If (Not String.IsNullOrWhiteSpace(txtToppingPrice2.Text)) AndAlso IsNumeric(txtToppingPrice2.Text) Then
                    .ToppingPrice2 = CDec(txtToppingPrice2.Text.Trim())
                End If
                If (Not String.IsNullOrWhiteSpace(txtToppingPrice3.Text)) AndAlso IsNumeric(txtToppingPrice3.Text) Then
                    .ToppingPrice3 = CDec(txtToppingPrice3.Text.Trim())
                End If

                If (Not String.IsNullOrWhiteSpace(txtDeliveryPrice.Text)) AndAlso IsNumeric(txtDeliveryPrice.Text) Then
                    .DeliveryPrice = CDec(txtDeliveryPrice.Text.Trim())
                End If
                If (Not String.IsNullOrWhiteSpace(txtCollectionPrice.Text)) AndAlso IsNumeric(txtCollectionPrice.Text) Then
                    .CollectionPrice = CDec(txtCollectionPrice.Text.Trim())
                End If

                '.MenuImage = ThumbImage.ImageUrl
                '.LargeImage = LargeImage.ImageUrl

                If chkLinkDressingOrTopping.Checked AndAlso Not IsNothing(ddlMenuLink1.SelectedValue) AndAlso IsNumeric(ddlMenuLink1.SelectedValue) Then
                    .OptionId1 = CInt(ddlMenuLink1.SelectedValue)
                Else
                    .OptionId1 = 0
                End If

                If chkLinkMenu.Checked AndAlso Not IsNothing(ddlMenuLink2.SelectedValue) AndAlso IsNumeric(ddlMenuLink2.SelectedValue) Then
                    .OptionId2 = CInt(ddlMenuLink2.SelectedValue)
                Else
                    .OptionId2 = 0
                End If

                .PromotionText = PromotionText.Text
                .Remarks = Remarks.Text
            End With

            Try
                If vBusiness.SaveMenuItem(vMenuItem) Then
                    LoadMenuItems(CategoryID)
                    ShowMessageBox("[" & vMenuItem.Name & "] " & "has been saved successfully.")
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try

            HideMenuItemEditorModal()
        End Sub

        Private Sub LoadMenuItems(vCategoryId As Integer)
            Dim bzMenu As New MenuBusinessLogic()
            Try
                Dim menuItems As IEnumerable(Of CsMenuItem)
                If vCategoryId > 0 Then
                    menuItems = bzMenu.GetMenuItemsByCategory(vCategoryId)
                Else
                    menuItems = bzMenu.GetAllMenuItems()
                End If

                gvMenuItem.DataSource = menuItems
                gvMenuItem.DataBind()
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Sub LoadMenuItem(vItemId As Integer)
            Try
                Dim vBusiness As New MenuBusinessLogic()
                Dim menuItem As CsMenuItem = vBusiness.GetMenuItem(vItemId)
                If menuItem IsNot Nothing Then
                    With menuItem
                        hdnCategoryID.Value = .CategoryID
                        If Not IsNothing(.Category) Then
                            txtCategoryName.Text = .Category.Name
                        End If

                        If ddlCategories.Enabled Then
                            ddlCategories.SelectedIndex = 0
                            For Each obj As Object In ddlCategories.Items
                                If TypeOf (obj) Is ListItem Then
                                    Dim dataItem As ListItem = DirectCast(obj, ListItem)
                                    If Not IsNothing(dataItem) AndAlso dataItem.Value = .CategoryID.ToString() Then
                                        ddlCategories.SelectedValue = .CategoryID
                                        Exit For
                                    End If
                                End If
                            Next

                            'Dim boundCategories As IEnumerable(Of CsMenuCategory) = DirectCast(ddlCategories.DataSource, IEnumerable(Of CsMenuCategory))
                            'If Not IsNothing(boundCategories) AndAlso boundCategories.Any(Function(c) c.ID = .CategoryID) Then
                            '    ddlCategories.SelectedValue = .CategoryID
                            'End If
                        End If

                        txtMenuItemName.Text = .Name
                        txtItemPosition.Text = .ItemPosition
                        txtPreparationTime.Text = .PreparationTime
                        chkActive.Checked = .IsActive
                        chkBaseSelection.Checked = .HasBase
                        txtToppingPrice1.Text = FormatNumber(.ToppingPrice1, 2)
                        txtToppingPrice2.Text = FormatNumber(.ToppingPrice2, 2)
                        txtToppingPrice3.Text = FormatNumber(.ToppingPrice3, 2)
                        txtDeliveryPrice.Text = FormatNumber(.DeliveryPrice, 2)
                        txtCollectionPrice.Text = FormatNumber(.CollectionPrice, 2)

                        PromotionText.Text = .PromotionText
                        Remarks.Text = .Remarks
                        hdnMenuItemID.Value = .ID

                        If .HasDressing OrElse .HasTopping Then
                            chkLinkDressingOrTopping.Checked = True
                        Else
                            chkLinkDressingOrTopping.Checked = False
                        End If

                        If .HasSubMenu Then
                            radMultipleSize.Checked = True
                            radOneSize.Checked = False
                        Else
                            radMultipleSize.Checked = False
                            radOneSize.Checked = True
                        End If

                        Dim linkingItems1 As List(Of CsMenuOption) = DirectCast(ddlMenuLink1.DataSource, List(Of CsMenuOption))
                        If Not IsNothing(linkingItems1) AndAlso .OptionId1 AndAlso linkingItems1.Any(Function(c) c.ID = .OptionId1) Then
                            ddlMenuLink1.SelectedValue = .OptionId1
                            chkLinkDressingOrTopping.Checked = True
                        End If

                        Dim linkingItems2 As List(Of CsMenuOption) = DirectCast(ddlMenuLink2.DataSource, List(Of CsMenuOption))
                        If Not IsNothing(linkingItems2) AndAlso .OptionId2 > 0 AndAlso linkingItems2.Any(Function(c) c.ID = .OptionId2) Then
                            ddlMenuLink2.SelectedValue = .OptionId2
                            chkLinkMenu.Checked = True
                        End If

                    End With
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Const DRESSING_LINK_ID As Integer = 101
        Private Const TOPPING_LINK_ID As Integer = 102

        Private Sub ShowMenuItemEditorModal(Optional vMenuItemId As Integer = 0)

            InitMenuLink1()
            InitMenuLink2()

            If vMenuItemId = 0 Then

            Else
                LoadMenuItem(vMenuItemId)
            End If

            Dim sbJsScript As New StringBuilder()
            With sbJsScript
                .AppendLine("<script type='text/javascript'>")
                .AppendLine("    $('#addMenuItemEditorModal').modal('show');")
                .AppendLine("</script>")
            End With

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ShowMenuItemEditorModalScript", sbJsScript.ToString(), False)
        End Sub

        Private Sub InitMenuLink2()

            Dim bzMenu As New MenuBusinessLogic()
            Dim menuOptions As IEnumerable(Of CsMenuOption) = bzMenu.GetAllMenuOptions()
            Dim lstOptions As List(Of CsMenuOption) = New List(Of CsMenuOption)(menuOptions)

            lstOptions.OrderBy(Function(x) x.Position)
            lstOptions.Insert(0, New CsMenuOption() With {.ID = 0, .Name = "[Select Options]"})

            ddlMenuLink2.DataSource = lstOptions
            ddlMenuLink2.DataTextField = "Name"
            ddlMenuLink2.DataValueField = "ID"
            ddlMenuLink2.DataBind()
        End Sub

        Private Sub InitMenuLink1()
            Dim bzMenu As New MenuBusinessLogic()
            Dim menuOptions As IEnumerable(Of CsMenuOption) = bzMenu.GetAllMenuOptions()
            Dim lstOptions As List(Of CsMenuOption) = New List(Of CsMenuOption)(menuOptions)

            lstOptions.OrderBy(Function(x) x.Position)
            lstOptions.Insert(0, New CsMenuOption() With {.ID = 0, .Name = "[Select Options]"})
            lstOptions.Insert(1, New CsMenuOption() With {.ID = DRESSING_LINK_ID, .Name = "Dressing"})
            lstOptions.Insert(2, New CsMenuOption() With {.ID = TOPPING_LINK_ID, .Name = "Topping"})

            ddlMenuLink1.DataSource = lstOptions
            ddlMenuLink1.DataTextField = "Name"
            ddlMenuLink1.DataValueField = "ID"
            ddlMenuLink1.DataBind()
        End Sub

        Private Sub HideMenuItemEditorModal()
            Dim sbScript As New StringBuilder()
            With sbScript
                .AppendLine("<script type='text/javascript'>")
                .AppendLine("    $('#addMenuItemEditorModal').modal('hide');")
                .AppendLine("</script>")
            End With

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "HideMenuItemEditorModalScript", sbScript.ToString(), False)
        End Sub

        Protected Sub ShowMessageBox(ByVal message As String)
            Dim script As String = ""
            script = String.Format("alert('{0}');", message)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", script, True)
        End Sub

        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Response.RedirectTo("Category.aspx")
        End Sub
    End Class
End Namespace