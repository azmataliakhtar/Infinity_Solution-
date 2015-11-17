Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI.Utils
Imports INF.Web.UI.Logging.Log4Net

Namespace Ajax
    Public Class GetMaxDressing
        Inherits System.Web.UI.Page

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            _log.Info("BEGIN")

            Dim menuBll As New MenuBusinessLogic()
            Try
                Dim categoryId As Int32 = WebUtil.GetParameterValueAsString(Page.Request, "id")
                Dim menuInfo As CsMenuCategory = menuBll.GetMenuCategoryByID(categoryId)
                If menuInfo IsNot Nothing Then
                    Response.Write(menuInfo.MaxDressing)
                End If

            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try

            _log.Info("END")
        End Sub

    End Class

End Namespace