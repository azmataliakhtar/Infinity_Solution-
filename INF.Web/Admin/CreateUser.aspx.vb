
Imports INF.Web.Data
Imports INF.Web.Data.BLL
Imports INF.Web.Data.Entities
Imports INF.Web.UI

Namespace Admin
    Partial Class CreateUser
        Inherits AdminPage

        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then

                UserRolesDropDownList.Items.Add(New ListItem() With {.Value = CStr(CShort(UserRoles.Administrator)), .Text = "Administrator"})
                UserRolesDropDownList.Items.Add(New ListItem() With {.Value = CStr(CShort(UserRoles.RestrictedUser)), .Text = "RestrictedUser"})
                UserRolesDropDownList.SelectedValue = CStr(CShort(UserRoles.RestrictedUser))
            End If
        End Sub

        Protected Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
            If Page.IsValid Then
                Dim business As New UserBusinessLogic
                If business.ExistedUserName(UserNameTextBox.Text.Trim()) Then
                    Messages.Text = "There is already user registed with the same username"
                    MessageBox.Visible = True
                    Return
                End If

                Dim user As New CsUser With {
                        .UserName = UserNameTextBox.Text.Trim(),
                        .Email = EmailTextBox.Text.Trim(),
                        .Password = CryptoUtility.EncryptText(PasswordTextBox.Text.Trim()),
                        .RoleID = CInt(UserRolesDropDownList.SelectedValue),
                        .IsActived = False
                        }
                business.SaveUser(user, "epos")
                Response.RedirectTo("Users.aspx")
            End If
        End Sub

        Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
            Response.RedirectTo("Users.aspx")
        End Sub
    End Class
End Namespace