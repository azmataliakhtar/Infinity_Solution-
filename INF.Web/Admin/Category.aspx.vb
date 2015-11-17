Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports System.IO
Imports INF.Web.UI.Logging.Log4Net

Partial Class Admin_Category
    Inherits AdminPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadMenuCategories()
        End If
    End Sub

    Protected Sub SaveChanges_Click(sender As Object, e As System.EventArgs) Handles btnSaveChanges.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim vBusiness As New MenuBusinessLogic()
        Dim vCat As New CsMenuCategory()
        With vCat
            .Name = txtName.Text.Trim()

            If Not String.IsNullOrEmpty(txtPosition.Text.Trim()) AndAlso IsNumeric(txtPosition.Text.Trim()) Then
                .ItemPosition = CInt(txtPosition.Text.Trim())
            Else
                .ItemPosition = 0
            End If

            .IsActive = IsActive.Checked

            If (Not String.IsNullOrWhiteSpace(MenuCategoryID.Value)) AndAlso IsNumeric(MenuCategoryID.Value) Then
                .ID = CDec(MenuCategoryID.Value)

                Dim existedCategory As CsMenuCategory = vBusiness.GetMenuCategoryByID(CInt(MenuCategoryID.Value))
                If Not IsNothing(existedCategory) Then
                    .NormalImage = existedCategory.NormalImage
                    .MouseOverImage = existedCategory.MouseOverImage
                End If
            End If

            If Not String.IsNullOrEmpty(txtMaxOfDressing.Text.Trim()) AndAlso IsNumeric(txtMaxOfDressing.Text.Trim()) Then
                .MaxDressing = CDec(txtMaxOfDressing.Text.Trim())
            Else
                .MaxDressing = 0
            End If

            .IsDeal = chkIsDeal.Checked
            .ExclOnlineDiscount = chkExclDiscount.Checked
            .IsAvailableForDeal = chkIsAvailableForDeal.Checked
        End With

        Try
            If vBusiness.SaveMenuCategory(vCat) Then
                LoadMenuCategories()
                ShowMessageBox("[" & vCat.Name & "] " & "has been saved successfully.")
            End If
        Catch ex As Exception
            _log.Error(ex)
            ShowMessageBox("Message: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try

        HideCategoryEditorModal()
    End Sub

    Private Sub btnNewCategory_Click(sender As Object, e As EventArgs) Handles btnNewCategory.Click
        ShowCategoryEditorModal()
    End Sub

    Private Sub gvCategories_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCategories.RowCommand
        Dim index = -1
        If Not IsNothing(e.CommandArgument) AndAlso IsNumeric(e.CommandArgument) Then
            index = CInt(e.CommandArgument)
        End If

        If index < 0 Then
            Exit Sub
        End If

        Dim catId = CInt(gvCategories.DataKeys(index).Value)
        Dim bizMenu = New MenuBusinessLogic()
        Dim cat = bizMenu.GetMenuCategoryByID(catId)

        Select Case e.CommandName
            Case "SelectCategory"
                If Not IsNothing(cat) Then
                    MenuCategoryID.Value = cat.ID
                    txtName.Text = cat.Name
                    txtPosition.Text = cat.ItemPosition
                    IsActive.Checked = cat.IsActive
                    txtMaxOfDressing.Text = cat.MaxDressing
                    'imgNormalBackground.ImageUrl = cat.NormalImage
                    'imgHoverBackground.ImageUrl = cat.MouseOverImage
                    chkExclDiscount.Checked = cat.ExclOnlineDiscount
                    chkIsDeal.Checked = cat.IsDeal
                    chkIsAvailableForDeal.Checked = cat.IsAvailableForDeal

                    ShowCategoryEditorModal()
                End If
                Exit Select

            Case "SelectMenuItems"
                If Not IsNothing(cat) Then
                    Response.RedirectTo("MenuItem.aspx?CategoryID=" & cat.ID)
                Else
                    ShowMessageBox("[" & cat.Name & "] " & "could not be found!")
                End If
                Exit Select

            Case "DeleteCategory"
                If Not IsNothing(cat) Then
                    bizMenu.DeleteMenuCategory(cat)
                    LoadMenuCategories()

                    ShowMessageBox("[" & cat.Name & "] " & "has been deleted successfully.")
                Else
                    ShowMessageBox("[" & cat.Name & "] " & "could not be found!")
                End If
                Exit Select
            Case Else
                Exit Select
        End Select
    End Sub

    Protected Function GetCssClass(sId As String) As String
        If sId = MenuCategoryID.Value Then
            Return "active"
        Else
            Return ""
        End If
    End Function

    Private Sub LoadMenuCategories()
        Try
            Dim vBusiness As New MenuBusinessLogic()
            Dim vMenuCategories As IEnumerable(Of CsMenuCategory) = vBusiness.GetAllMenuCategories()

            If Not IsNothing(vMenuCategories) Then
                vMenuCategories = vMenuCategories.OrderBy(Function(c) c.ItemPosition)
            End If

            gvCategories.DataSource = vMenuCategories
            gvCategories.DataBind()

        Catch ex As Exception
            _log.Error(ex)
        End Try
    End Sub

    Private Sub ShowCategoryEditorModal()
        Dim sbScript As New StringBuilder()
        With sbScript
            .AppendLine("<script type='text/javascript'>")
            .AppendLine("    $('#addNewCategoryModal').modal('show');")
            .AppendLine("</script>")
        End With

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ShowAddNewCategoryModalScript", sbScript.ToString(), False)
    End Sub

    Private Sub HideCategoryEditorModal()
        Dim sbScript As New StringBuilder()
        With sbScript
            .AppendLine("<script type='text/javascript'>")
            .AppendLine("    $('#addNewCategoryModal').modal('hide');")
            .AppendLine("</script>")
        End With

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "HideAddNewCategoryModalScript", sbScript.ToString(), False)
    End Sub

#Region "Upload Images"
    
    'Private Sub AjaxFileUpload1_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles AjaxFileUpload1.UploadedComplete
    '    If AjaxFileUpload1.HasFile Then
    '        If e.filename.Contains("jpg") OrElse e.filename.Contains("jpeg") OrElse _
    '        e.filename.Contains("png") Then

    '            Dim folderToUpload As String = MENU_CATEGORY_IMAGES_FOLDER
    '            Dim serverDir = HttpContext.Current.Server.MapPath(MENU_CATEGORY_IMAGES_FOLDER)
    '            If Not Directory.Exists(serverDir) Then
    '                Try
    '                    Directory.CreateDirectory(serverDir)
    '                Catch ex As Exception
    '                End Try
    '            End If

    '            If Not folderToUpload.EndsWith("/") Then
    '                folderToUpload = folderToUpload & "/"
    '            End If

    '            Dim url As String = folderToUpload & Path.GetFileName(e.filename)
    '            AjaxFileUpload1.SaveAs(MapPath(url))

    '            imgNormalBackground.ImageUrl = url
    '        End If
    '    End If
    'End Sub

#End Region

    Protected Sub ShowMessageBox(ByVal message As String)
        Dim script As String = ""
        script = String.Format("alert('{0}');", message)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", script, True)
    End Sub
End Class
