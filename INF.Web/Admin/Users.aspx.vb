
Imports INF.Web.Data
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI

Namespace Admin

    Partial Class Users
        Inherits AdminPage

        Protected Property ShowAddUserModalPopupToken() As String
            Get
                If Not IsNothing(Session("ShowAddUserModalPopupToken")) Then
                    Return CStr(Session("ShowAddUserModalPopupToken"))
                End If
                Return String.Empty
            End Get
            Set(value As String)
                Session("ShowAddUserModalPopupToken") = value
            End Set
        End Property

        Protected Property EditUserID() As Integer
            Get
                If Not IsNothing(Session("EditUserID")) Then
                    Return CInt(Session("EditUserID"))
                End If
                Return 0
            End Get
            Set(value As Integer)
                Session("EditUserID") = value
            End Set
        End Property

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                LoadAllUsers()

                UserRolesDropDownList.Items.Add(New ListItem() With {.Value = CStr(CShort(UserRoles.Administrator)), .Text = "Administrator"})
                UserRolesDropDownList.Items.Add(New ListItem() With {.Value = CStr(CShort(UserRoles.Manager)), .Text = "Manager"})
                UserRolesDropDownList.Items.Add(New ListItem() With {.Value = CStr(CShort(UserRoles.RestrictedUser)), .Text = "RestrictedUser"})
                UserRolesDropDownList.SelectedValue = CStr(CShort(UserRoles.RestrictedUser))

                ddlEditUserRole.Items.Add(New ListItem() With {.Value = CStr(CShort(UserRoles.Administrator)), .Text = "Administrator"})
                ddlEditUserRole.Items.Add(New ListItem() With {.Value = CStr(CShort(UserRoles.Manager)), .Text = "Manager"})
                ddlEditUserRole.Items.Add(New ListItem() With {.Value = CStr(CShort(UserRoles.RestrictedUser)), .Text = "RestrictedUser"})
            End If
        End Sub

        Private Sub LoadAllUsers()
            Dim business As New UserBusinessLogic
            Dim allUsers As IEnumerable(Of CsUser) = business.GetAllUsers(False).ToList()
            If Not IsNothing(allUsers) Then
                ltrNumberOfUsers.Text = allUsers.Count().ToString() & " Users"
                UsersGridView.DataSource = allUsers
                UsersGridView.DataBind()
            Else
                ltrNumberOfUsers.Text = "0 Users"
            End If
        End Sub

        Protected Sub UsersDataGrid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles UsersGridView.PageIndexChanged
            If e.NewPageIndex < UsersGridView.PageCount AndAlso e.NewPageIndex >= 0 Then
                'UsersDataGrid.CurrentPageIndex = e.NewPageIndex
            End If
            LoadAllUsers()
        End Sub

        Protected Sub CreateUserButton_Click(sender As Object, e As EventArgs) Handles CreateUserButton.Click
            Dim sbScript As New StringBuilder()
            With sbScript
                .AppendLine("<script type='text/javascript'>")
                .AppendLine("    $('#addNewUserModal').modal('show');")
                .AppendLine("</script>")
            End With

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ShowAddNewUserModalScript", sbScript.ToString(), False)
        End Sub

        Protected Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
            If Page.IsValid Then
                Dim business As New UserBusinessLogic
                If business.ExistedUserName(UserNameTextBox.Text.Trim()) Then
                    Messages.Text = "There is already user registed with the same username"
                    MessageBox.Visible = True
                    upUsersGrid.Update()
                    Return
                End If

                Dim usr As New CsUser With {
                        .UserName = UserNameTextBox.Text.Trim(),
                        .Email = EmailTextBox.Text.Trim(),
                        .Password = CryptoUtility.EncryptText(PasswordTextBox.Text.Trim()),
                        .RoleID = CInt(UserRolesDropDownList.SelectedValue),
                        .LastName = txtLastName.Text.Trim(),
                        .FirstName = txtFirstName.Text.Trim(),
                        .IsActived = True
                        }
                business.SaveUser(usr, CurrentUserName())
                LoadAllUsers()

                Dim sbScript As New StringBuilder()
                With sbScript
                    .AppendLine("<script type='text/javascript'>")
                    .AppendLine("    alert('New User has been Added successfully.');")
                    .AppendLine("    $('#addNewUserModal').modal('hide');")
                    .AppendLine("</script>")
                End With

                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "HideAddNewUserModalScript", sbScript.ToString(), False)
            End If
        End Sub

        Private Sub UsersDataGrid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles UsersGridView.RowCommand
            Dim index As Integer = CInt(e.CommandArgument)
            Select Case e.CommandName
                Case "EditUser"
                    Dim usrId As Integer = CInt(UsersGridView.DataKeys(index).Value)
                    Dim business As New UserBusinessLogic
                    Dim usr As CsUser = business.GetUser(usrId)

                    If Not IsNothing(usr) Then
                        EditUserID = usr.ID
                        txtEditUserName.Text = usr.UserName
                        txtEditEmail.Text = usr.Email
                        ddlEditUserRole.SelectedValue = CStr(usr.RoleID)
                        txtEditLastName.Text = usr.LastName
                        txtEditFirstName.Text = usr.FirstName
                        chkActivated.Checked = usr.IsActived

                        Dim sbScript As New StringBuilder()
                        With sbScript
                            .AppendLine("<script type='text/javascript'>")
                            .AppendLine("    $('#editUserModal').modal('show');")
                            .AppendLine("</script>")
                        End With

                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ShowEditUserModalScript", sbScript.ToString(), False)
                    End If
                    Exit Select
                Case "ChangePassword"
                    Dim usrId As Integer = CInt(UsersGridView.DataKeys(index).Value)
                    Dim business As New UserBusinessLogic
                    Dim usr As CsUser = business.GetUser(usrId)

                    If Not IsNothing(usr) Then
                        EditUserID = usr.ID
                        txtChangePassUserName.Text = usr.UserName
                        txtChangePassNewPassword.Text = String.Empty
                        txtChangePassNewPasswordConfirm.Text = String.Empty

                        Dim sbScript As New StringBuilder()
                        With sbScript
                            .AppendLine("<script type='text/javascript'>")
                            .AppendLine("    $('#changePasswordModal').modal('show');")
                            .AppendLine("</script>")
                        End With

                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ShowChangePasswordModalScript", sbScript.ToString(), False)
                    End If
                    Exit Select
            End Select
        End Sub

        Protected Sub btnSaveChangePassword_Click(sender As Object, e As EventArgs) Handles btnSaveChangePassword.Click
            If Page.IsValid Then
                Dim business As New UserBusinessLogic
                Dim usr As CsUser = business.GetUser(EditUserID)

                If Not IsNothing(usr) Then
                    If Not business.ChangePassword(usr.UserName, txtChangePassOldPassword.Text, txtChangePassNewPassword.Text, CurrentUserName()) Then
                        Messages.Text = "The old password is not correct!"
                        MessageBox.Visible = True
                        Return
                    End If

                    Dim sbScript As New StringBuilder()
                    With sbScript
                        .AppendLine("<script type='text/javascript'>")
                        .AppendLine("    alert('The password has been changed successfully.');")
                        .AppendLine("    $('#changePasswordModal').modal('hide');")
                        .AppendLine("</script>")
                    End With

                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "HideChangePasswordModalScript", sbScript.ToString(), False)
                End If
            End If
        End Sub

        Protected Sub SaveEditButton_Click(sender As Object, e As EventArgs) Handles SaveEditButton.Click
            If Page.IsValid Then
                Dim business As New UserBusinessLogic
                If business.ExistedUserName(EditUserID, txtEditEmail.Text.Trim()) Then
                    Messages.Text = "There is already user registed with the same username."
                    MessageBox.Visible = True
                    upUsersGrid.Update()
                    Return
                End If

                Dim usr As CsUser = business.GetUser(EditUserID)
                If Not IsNothing(usr) Then
                    usr.UserName = txtEditUserName.Text.Trim()
                    usr.Email = txtEditEmail.Text.Trim()
                    usr.RoleID = CInt(ddlEditUserRole.SelectedValue)
                    usr.LastName = txtEditLastName.Text.Trim()
                    usr.FirstName = txtEditFirstName.Text.Trim()
                    usr.IsActived = chkActivated.Checked

                    business.SaveUser(usr, CurrentUserName())
                    LoadAllUsers()

                    Dim sbScript As New StringBuilder()
                    With sbScript
                        .AppendLine("<script type='text/javascript'>")
                        .AppendLine("    alert('The changes has been saved successfully.');")
                        .AppendLine("    $('#editUserModal').modal('hide');")
                        .AppendLine("</script>")
                    End With

                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "HideEditUserModalScript", sbScript.ToString(), False)
                End If

            End If
        End Sub
    End Class
End Namespace