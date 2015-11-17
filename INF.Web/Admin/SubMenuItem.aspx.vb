Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI
Imports System.Reflection

Namespace Admin
    Public Class SubMenuItem
        Inherits AdminPage

        'Private Const DRESSING_LINK_ID As Integer = 101
        'Private Const TOPPING_LINK_ID As Integer = 102
        Private ReadOnly _log As log4net.ILog = log4net.LogManager.GetLogger(GetType(SubMenuItem))

        Protected ReadOnly Property CategoryID() As Integer
            Get
                Dim categoryIdStr As String = Page.Request.QueryString("CategoryId")
                If Not String.IsNullOrEmpty(categoryIdStr) AndAlso IsNumeric(categoryIdStr) Then
                    Return CInt(categoryIdStr)
                End If
                Return 0
            End Get
        End Property

        Protected ReadOnly Property MenuID() As Integer
            Get
                Dim menuIdStr As String = Page.Request.QueryString("MenuId")
                If Not String.IsNullOrEmpty(menuIdStr) AndAlso IsNumeric(menuIdStr) Then
                    Return CInt(menuIdStr)
                End If
                Return 0
            End Get
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                phMessage.Visible = False
                LoadSubMenuItems()
            End If
        End Sub

        Private Sub btnNewMenuItem_Click(sender As Object, e As EventArgs) Handles btnNewMenuItem.Click

            ltrMenuItemEditorHeaderText.Text = "Add New Sub Menu Item"

            hdnSubMenuItemId.Value = "0"
            txtSubMenuItemName.Text = ""
            txtItemPosition.Text = "0"
            txtPreparationTime.Text = "0"
            chkActive.Checked = True

            txtToppingPrice1.Text = FormatNumber(0, 2)
            txtToppingPrice2.Text = FormatNumber(0, 2)
            txtToppingPrice3.Text = FormatNumber(0, 2)
            txtDeliveryPrice.Text = FormatNumber(0, 2)
            txtCollectionPrice.Text = FormatNumber(0, 2)

            ShowSubMenuItemEditorModal()
        End Sub

        Private Sub gvSubMenuItem_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSubMenuItem.RowCommand
            Dim index = -1
            If Not IsNothing(e.CommandArgument) AndAlso IsNumeric(e.CommandArgument) Then
                index = CInt(e.CommandArgument)
            End If

            If index < 0 Then
                Exit Sub
            End If

            Dim itemId = CInt(gvSubMenuItem.DataKeys(index).Value)
            Dim bizMenu = New MenuBusinessLogic()

            Try
                'Dim menuItem = bizMenu.GetMenuItem(itemId)
                Select Case e.CommandName
                    Case "SelectMenuItem"
                        ltrMenuItemEditorHeaderText.Text = "Edit Existing Sub Menu Item"
                        ShowSubMenuItemEditorModal(itemId)
                        Exit Select
                    Case "DeleteMenuItem"
                        Dim itemToDelete = bizMenu.GetSubMenuItem(itemId)
                        If Not IsNothing(itemToDelete) Then
                            If bizMenu.DeleteSubMenuItem(itemToDelete.ID) Then
                                LoadSubMenuItems()
                                ShowMessageBox("[" & itemToDelete.Name & "] " & "has been deleted successfully.")
                            Else
                                ShowMessageBox("[" & itemToDelete.Name & "] " & "has NOT been deleted.")
                            End If
                        Else
                            ShowMessageBox("[" & itemToDelete.Name & "] " & "could not be found!")
                        End If
                        Exit Select

                    Case Else
                        Exit Select
                End Select
            Catch ex As Exception
                _log.Error(ex.Message & vbCrLf & ex.StackTrace)
            End Try
        End Sub

        Private Sub btnSaveChanges_Click(sender As Object, e As EventArgs) Handles btnSaveChanges.Click
            If Not Page.IsValid Then
                Exit Sub
            End If

            If String.IsNullOrEmpty(hdnMenuItemId.Value) OrElse Not IsNumeric(hdnMenuItemId.Value) Then
                Exit Sub
            End If

            Dim bzMenu As New MenuBusinessLogic()
            Try
                Dim subMenuItem As CsSubMenuItem
                If Not String.IsNullOrEmpty(hdnSubMenuItemId.Value) AndAlso IsNumeric(hdnSubMenuItemId.Value) Then
                    subMenuItem = bzMenu.GetSubMenuItemByID(CDec(hdnSubMenuItemId.Value))
                Else
                    subMenuItem = New CsSubMenuItem()
                End If

                'If IsNothing(subMenuItem) Then
                '    subMenuItem = New CsSubMenuItem()
                'End If

                Dim checkSubItem As CsSubMenuItem = bzMenu.FindSubMenuItemByName(CInt(hdnMenuItemId.Value), txtSubMenuItemName.Text.Trim())
                If Not IsNothing(checkSubItem) AndAlso checkSubItem.ID > 0 AndAlso subMenuItem.ID <> checkSubItem.ID Then
                    'ShowMessageBox("There is already an item with the same name. Please enter different name.")
                    ltrMessage.Text = "There is already an item with the same name. Please enter different name."
                    phMessage.Visible = True
                    Exit Sub
                Else
                    ltrMessage.Text = ""
                    phMessage.Visible = False
                End If

                With subMenuItem
                    .MenuID = CInt(hdnMenuItemId.Value)
                    .ID = CDec(hdnSubMenuItemId.Value)
                    .Name = txtSubMenuItemName.Text.Trim()
                    If (Not String.IsNullOrWhiteSpace(txtItemPosition.Text)) AndAlso IsNumeric(txtItemPosition.Text) Then
                        .Position = CInt(txtItemPosition.Text.Trim())
                    Else
                        .Position = 0
                    End If
                    If Not String.IsNullOrEmpty(txtPreparationTime.Text) AndAlso IsNumeric(txtPreparationTime.Text) Then
                        .PreparationTime = CInt(txtPreparationTime.Text.Trim())
                    Else
                        .PreparationTime = 0
                    End If

                    .IsActive = chkActive.Checked

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
                End With

                If bzMenu.SaveSubMenuItem(subMenuItem) Then
                    LoadSubMenuItems()
                    ShowMessageBox("[" & subMenuItem.Name & "] " & "has been saved successfully.")
                End If

            Catch ex As Exception
                _log.Error(ex.Message & vbCrLf & ex.StackTrace)
            End Try

            HideSubMenuItemEditorModal()
        End Sub

        Private Sub LoadSubMenuItems()
            Dim bzMenu As New MenuBusinessLogic()
            Try
                Dim subMenuItems As IEnumerable(Of CsSubMenuItem) = bzMenu.GetSubMenuItemsByMenuID(MenuID)
                gvSubMenuItem.DataSource = subMenuItems
                gvSubMenuItem.DataBind()
            Catch ex As Exception
                _log.Error(ex.Message & vbCrLf & ex.StackTrace)
            End Try
        End Sub

        Private Sub LoadSubMenuItem(ByVal vSubMenuItemId As Integer)
            Dim vBusiness As New MenuBusinessLogic()

            Try
                Dim subMenuItem As CsSubMenuItem = vBusiness.GetSubMenuItemByID(vSubMenuItemId)
                If subMenuItem IsNot Nothing Then
                    With subMenuItem
                        If Not IsNothing(.MenuItem) Then
                            txtMenuItem.Text = .MenuItem.Name
                            hdnMenuItemId.Value = .MenuItem.ID
                        End If

                        hdnSubMenuItemId.Value = .ID
                        txtSubMenuItemName.Text = .Name
                        txtItemPosition.Text = .Position
                        txtPreparationTime.Text = .PreparationTime
                        chkActive.Checked = .IsActive

                        txtToppingPrice1.Text = FormatNumber(.ToppingPrice1, 2)
                        txtToppingPrice2.Text = FormatNumber(.ToppingPrice2, 2)
                        txtToppingPrice3.Text = FormatNumber(.ToppingPrice3, 2)
                        txtDeliveryPrice.Text = FormatNumber(.DeliveryPrice, 2)
                        txtCollectionPrice.Text = FormatNumber(.CollectionPrice, 2)
                    End With
                End If
            Catch ex As Exception
                _log.Error(ex)
            End Try
        End Sub

        Private Sub ShowSubMenuItemEditorModal(Optional ByVal vSubMenuItemId As Integer = 0)
            If MenuID = 0 Then
                Exit Sub
            End If

            Dim bzMenu As New MenuBusinessLogic()
            Dim menuItem As CsMenuItem = bzMenu.GetMenuItem(MenuID)
            If Not IsNothing(menuItem) AndAlso menuItem.ID > 0 Then
                txtMenuItem.Text = menuItem.Name
                hdnMenuItemId.Value = menuItem.ID
            Else
                Exit Sub
            End If

            If vSubMenuItemId > 0 Then
                LoadSubMenuItem(vSubMenuItemId)
            End If

            Dim sbJsScript As New StringBuilder()
            With sbJsScript
                .AppendLine("<script type='text/javascript'>")
                .AppendLine("    $('#addMenuItemEditorModal').modal('show');")
                .AppendLine("</script>")
            End With

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ShowSubMenuItemEditorModalScript", sbJsScript.ToString(), False)
        End Sub

        Private Sub HideSubMenuItemEditorModal()
            Dim sbScript As New StringBuilder()
            With sbScript
                .AppendLine("<script type='text/javascript'>")
                .AppendLine("    $('#addMenuItemEditorModal').modal('hide');")
                .AppendLine("</script>")
            End With

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "HideSubMenuItemEditorModalScript", sbScript.ToString(), False)
        End Sub

        Protected Sub ShowMessageBox(ByVal message As String)
            Dim script As String = ""
            script = String.Format("alert('{0}');", message)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", script, True)
        End Sub

        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Response.RedirectTo("MenuItem.aspx")
        End Sub
    End Class
End Namespace