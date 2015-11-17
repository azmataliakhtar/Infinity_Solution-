Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports iTextSharp.text.pdf
Imports INF.Web.UI.Settings
Imports INF.Web.UI.Logging.Log4Net

Public Class Deals
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Property CategoryId() As Decimal
        Set(ByVal value As Decimal)
            ViewState("Deals_CategoryID") = value
        End Set
        Get
            If (Not (ViewState("Deals_CategoryID") Is Nothing)) AndAlso (Not String.IsNullOrWhiteSpace(ViewState("Deals_CategoryID").ToString())) Then
                Return CDec(ViewState("Deals_CategoryID"))
            End If
            Return 0
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _log.Info("BEGIN")

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If Not IsPostBack Then

            Dim vRemainingTime As Double = RemainingTime
            Call CheckShopStatus(vRemainingTime)

            LoadDealsCategories()
            LoadDealsDetails()
        End If

        _log.Info("END")
    End Sub

    Private Sub LoadDealsDetails()
        _log.Info("BEGIN")
        Try
            Dim vBusiness As New MenuBusinessLogic()
            Dim dealsDetails As IEnumerable(Of CsDealDetail) = vBusiness.GetDealDetailsByCategory(CategoryId, False)
            rptDealsDetails.DataSource = dealsDetails
            rptDealsDetails.DataBind()
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try
        _log.Info("END")
    End Sub

    Private Sub LoadDealsCategories()
        _log.Info("BEGIN")
        Try
            Dim vBusiness As New MenuBusinessLogic()
            Dim categories As IList(Of CsMenuCategory) = vBusiness.GetDealsMenuCategories().ToList()
            'rptDealsCategory.DataSource = categories
            'rptDealsCategory.DataBind()

            ' Keeps the first category ID
            'If categories IsNot Nothing Then
            '    Dim enumerator As IEnumerator(Of CsMenuCategory) = categories.GetEnumerator()
            '    If (enumerator.MoveNext()) Then
            '        Dim vMenuCategory As CsMenuCategory = enumerator.Current
            '        CategoryId = vMenuCategory.ID
            '    End If
            'End If

            If Not IsNothing(categories) AndAlso categories.Count > 0 Then
                CategoryId = categories(0).ID
            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try
        _log.Info("END")
    End Sub

    Private Function CheckShopStatus(ByVal seconds As Double) As Boolean
        _log.Info("BEGIN")
        Try
            Dim day As Integer = CType(Math.Floor(seconds / (60 * 60 * 24)), Integer)
            Dim hour As Integer = CType((Math.Floor(seconds / 3600) - (day * 24)), Integer)
            Dim minute As Integer = CType((Math.Floor(seconds / 60) - (day * 24 * 60) - (hour * 60)), Integer)

            If ((hour <= 0 And minute <= 0) Or hour > 18.5) Then
                phShoppingCart.Visible = False
                imgClosedSign.Visible = True
                'phShoppingCart.Visible = True
                'imgClosedSign.Visible = False

                _log.Info("END")
                Return False
            Else
                phShoppingCart.Visible = True
                imgClosedSign.Visible = False

                _log.Info("END")
                Return True
            End If

        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try

        _log.Info("END")
        Return False
    End Function

    Private Sub rptDealsDetails_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles rptDealsDetails.ItemCreated
        _log.Info("BEGIN")

        Try
            Dim vOrderImageButton As ImageButton = CType(e.Item.FindControl("ImgAdd2Cart"), ImageButton)
            'vOrderImageButton.ImageUrl = ResolveUrl(EPATheme.Current.Themes.AddToCartImageUrl)

            AddHandler vOrderImageButton.Command, AddressOf AddToCart_Command
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try

        _log.Info("END")
    End Sub

    Private Sub AddToCart_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        _log.Info("BEGIN")

        Try
            Dim detailId As Integer = IIf(IsNothing(e.CommandArgument), 0, CInt(e.CommandArgument))
            If detailId = 0 Then Exit Sub

            Dim vBusiness As New MenuBusinessLogic()
            Dim deal As CsDealDetail = vBusiness.GetDealDetailById(detailId, False)

            If Not IsNothing(deal) Then
                ltrDealName.Text = deal.Name

                hdnModalPopupName.Value = "__NEXT__"

                hdnDealSelected.Value = detailId
                hdnDealOptionsSelected.Value = ""
                mpeDealOptions.DynamicServiceMethod = "GetDealDetailById"
                mpeDealOptions.DynamicContextKey = detailId
                mpeDealOptions.Show()
            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try

        _log.Info("END")
    End Sub
    
    Private Sub rptDealsDetails_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDealsDetails.ItemDataBound
        _log.Info("BEGIN")

        Try
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim vOrderImageButton As ImageButton = CType(e.Item.FindControl("ImgAdd2Cart"), ImageButton)
                If IsNothing(vOrderImageButton) Then
                    Exit Sub
                End If

                'Dim boundItem As CsDealDetail = Nothing
                'If TypeOf e.Item.DataItem Is CsDealDetail Then
                '    boundItem = CType(e.Item.DataItem, CsDealDetail)
                'End If

                'If Not IsNothing(boundItem) Then
                '    vOrderImageButton.Attributes.Add("onclick", "showDealOptionsPopupModal(" & boundItem.ID & ")")
                'End If

                ' Check if shop is closed, hide order button
                If Not CheckShopStatus(RemainingTime) Then
                    vOrderImageButton.Visible = False
                Else
                    vOrderImageButton.Visible = True
                End If

            End If
        Catch ex As Exception
            _log.Error(ex)
            Response.RedirectTo(WebConstants.ERROR_PAGE)
        End Try

        _log.Info("END")
    End Sub
    
End Class