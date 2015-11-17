Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports AjaxControlToolkit
Imports INF.Web.Data.Entities
Imports INF.Web.Data.BLL
Imports INF.Web.UI
Imports INF.Web.UI.Settings
Imports INF.Web.Data.DAL.SqlClient
Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Logging.Log4Net

Partial Class Menu
    Inherits EPAPage

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Protected Property CategoryID() As Decimal
        Set(ByVal value As Decimal)
            ViewState("CategoryID") = value
        End Set
        Get
            If (Not (ViewState("CategoryID") Is Nothing)) AndAlso (Not String.IsNullOrWhiteSpace(ViewState("CategoryID").ToString())) Then
                Return CDec(ViewState("CategoryID"))
            End If
            Return 0
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _log.Info("BEGIN")

        Try
            If Not IsPostBack Then

                Response.Cache.SetCacheability(HttpCacheability.NoCache)

                Dim vRemainingTime As Double = RemainingTime
                Call CheckShopStatus(vRemainingTime)
                Call LoadMenuCategories()

                Dim categoryIdStr As String = Request.QueryString("CategoryId")
                If Not String.IsNullOrWhiteSpace(categoryIdStr) Then
                    If IsNumeric(categoryIdStr) Then
                        CategoryID = CDec(categoryIdStr)
                    End If
                End If

                Call LoadMenuItemsByCategory(CategoryID)
                'Call LoadAllMenuItems()
                Call BuildOptionPopups()
                Call LoadToppings()

            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Private _bzTopping As BzMenuTopping

    Private Sub LoadToppings()
        _log.Info("BEGIN")

        If IsNothing(_bzTopping) Then
            _bzTopping = New BzMenuTopping(WebsiteConfig.Instance.ConnectionString)
        End If

        Dim allToppings As IEnumerable(Of CsMenuTopping) = _bzTopping.GetAllMenuToppings()
        If Not IsNothing(allToppings) Then
            dlToppings.DataSource = allToppings
            dlToppings.DataBind()
        End If

        _log.Info("END")
    End Sub

#Region "Private Methods"

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
                'Return True
            Else
                phShoppingCart.Visible = True
                imgClosedSign.Visible = False

                _log.Info("END")
                Return True
            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
        Return False
    End Function

    Private Sub LoadMenuCategories()
        _log.Info("BEGIN")

        Try
            Dim vBusiness As New MenuBusinessLogic()
            Dim categories As IEnumerable(Of CsMenuCategory) = vBusiness.GetNormalMenuCategories()
            MenuCategoryRepeater.DataSource = categories
            MenuCategoryRepeater.DataBind()

            MenuCategoryRepeaterMobi.DataSource = categories
            MenuCategoryRepeaterMobi.DataBind()

            ' Keeps the first category ID
            If categories IsNot Nothing Then
                Dim enumerator As IEnumerator(Of CsMenuCategory) = categories.GetEnumerator()
                If (enumerator.MoveNext()) Then
                    Dim vMenuCategory As CsMenuCategory = enumerator.Current
                    CategoryID = vMenuCategory.ID
                End If
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub

    Private Sub LoadMenuItemsByCategory(catId As Decimal)
        _log.Info("BEGIN")

        Dim vBusiness As New MenuBusinessLogic()
        Try
            Dim vMenuItems As IEnumerable(Of CsMenuItem) = vBusiness.GetMenuItemsByCategory(catId)
            rptMenus.DataSource = vMenuItems
            rptMenus.DataBind()
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub


    Private Sub LoadAllMenuItems()
        _log.Info("BEGIN")

        Dim vBusiness As New MenuBusinessLogic()
        Try

            Dim categories As IEnumerable(Of CsMenuCategory) = vBusiness.GetNormalMenuCategories()
            Dim tempNr = categories.Count
            Dim menuArr As New Collection

            Dim enumerator As IEnumerator(Of CsMenuCategory) = categories.GetEnumerator()
            Dim catNr As Integer
            catNr = 0

            Try
                While (enumerator.MoveNext())
                    Dim tempCategory As CsMenuCategory = enumerator.Current
                    CategoryID = tempCategory.ID

                    'Dim tempCatItems As IEnumerable(Of CsMenuItem) = vBusiness.GetMenuItemsByCategory(CategoryID)
                    menuArr.Add(CategoryID, catNr)

                    'Dim allmenuCategory As HiddenField = CType(e.Item.FindControl("allmenuCategory"), HiddenField)

                    'Dim vMenuID As Integer = 0
                    'If Not IsNothing(hdMenuId) AndAlso IsNumeric(hdMenuId.Value) Then
                    'vMenuID = CInt(hdMenuId.Value)
                    'End If

                    'Dim vallmenuCategory As HiddenField = CType(AllMenuRepeater.Items(vIndex).FindControl("allmenuCategory"), HiddenField)
                    catNr = catNr + 1
                End While
            Catch ex As Exception
                _log.Error(ex)
            End Try



            ' AllMenuRepeater.DataSource = menuArr
            'AllMenuRepeater.DataBind()
        Catch ex As Exception
            _log.Error(ex)
        End Try

        ' Dim categories As IEnumerable(Of CsMenuCategory) = vBusiness.GetNormalMenuCategories()
        'MenuCategoryRepeater.DataSource = categories
        'MenuCategoryRepeater.DataBind()


        'If categories IsNot Nothing Then
        'Dim enumerator As IEnumerator(Of CsMenuCategory) = categories.GetEnumerator()
        'If (enumerator.MoveNext()) Then
        'Dim vMenuCategory As CsMenuCategory = enumerator.Current
        'CategoryID = vMenuCategory.ID
        'End If
        'End If



        _log.Info("END")
    End Sub

    Private Sub UpdatePriceInformation()
        _log.Info("BEGIN")

        Dim vBusiness As New MenuBusinessLogic()
        Try
            For index As Integer = 0 To rptMenus.Items.Count - 1

                Dim isDealType As Boolean = False
                Dim vSubMenuItemID As Decimal = 0

                Dim vSubMenuDropDownList As DropDownList = CType(rptMenus.Items(index).FindControl("SubMenuDropDownList"), DropDownList)
                'Dim vPriceLabel As Label = CType(MenuItemDataList.Items(index).FindControl("lblItemPrice"), Label)
                Dim vPriceLabel As Label = Nothing
                If IsDeliveryOrder Then
                    vPriceLabel = CType(rptMenus.Items(index).FindControl("lblDeliveryPrice"), Label)
                Else
                    vPriceLabel = CType(rptMenus.Items(index).FindControl("lblCollectionPrice"), Label)
                End If

                Dim vPriceHiddenField As HiddenField = Nothing
                If IsDeliveryOrder Then
                    vPriceHiddenField = CType(rptMenus.Items(index).FindControl("PriceHiddenField"), HiddenField)
                Else
                    vPriceHiddenField = CType(rptMenus.Items(index).FindControl("CollectionPriceHiddenField"), HiddenField)
                End If

                Dim vToppingPriceHiddenField As HiddenField = CType(rptMenus.Items(index).FindControl("hdToppingPrice"), HiddenField)

                If (vSubMenuDropDownList IsNot Nothing) AndAlso IsNumeric(vSubMenuDropDownList.SelectedValue) Then
                    ' Update the price against each datalist item, whether it is changed or not
                    vSubMenuItemID = CDec(vSubMenuDropDownList.SelectedValue)
                    Dim vSubMenuItem As CsSubMenuItem = vBusiness.GetSubMenuItemByID(vSubMenuItemID)

                    If vSubMenuItem IsNot Nothing Then
                        If IsDeliveryOrder Then
                            vPriceHiddenField.Value = FormatNumber(vSubMenuItem.DeliveryPrice, 2)
                        Else
                            vPriceHiddenField.Value = FormatNumber(vSubMenuItem.CollectionPrice, 2)
                        End If
                        vToppingPriceHiddenField.Value = FormatNumber(vSubMenuItem.ToppingPrice, 2)
                        vPriceLabel.Text = FormatCurrency(vPriceHiddenField.Value, 2)
                    Else
                        ' This item has no sub menu, check if its price is 0, then hide price tag
                        ' Show hide price if it is less than equal to 0.00
                        If vPriceHiddenField.Value <= 0 Then
                            vPriceLabel.Visible = False
                            isDealType = True
                        Else
                            vPriceLabel.Visible = True
                            isDealType = False
                        End If
                    End If
                End If

                Dim vItemNameLabel As Label = CType(rptMenus.Items(index).FindControl("ItemNameLabel"), Label)
                Dim vOrderImageButton As ImageButton = CType(rptMenus.Items(index).FindControl("ImgAdd2Cart"), ImageButton)
                Dim vMenuIdHiddenField As HiddenField = CType(rptMenus.Items(index).FindControl("hdMenuId"), HiddenField)
                Dim vItemName As String = vItemNameLabel.Text

                'vOrderImageButton.ImageUrl = ResolveUrl(EPATheme.Current.Themes.AddToCartImageUrl)

                If Not vSubMenuDropDownList Is Nothing Then
                    If vSubMenuDropDownList.Items.Count > 0 Then
                        vItemName = vItemName & "+" & vSubMenuDropDownList.SelectedItem.Text
                        vSubMenuItemID = CDec(vSubMenuDropDownList.SelectedValue)
                    End If

                End If

                If isDealType Then
                    'this page should navigate to deal detail page, potentially it is creat your own pizza or half and half pizza
                    vOrderImageButton.Attributes.Add("OnClick", "Navigate2Deal('" & vMenuIdHiddenField.Value & "')")
                Else

                    Dim optionPopupId1 As Integer = 0
                    Dim optionPopupId2 As Integer = 0

                    Dim hdnOptionPopUp1 As HiddenField = DirectCast(rptMenus.Items(index).FindControl("hdnOptionID1"), HiddenField)
                    Dim hdnOptionPopUp2 As HiddenField = DirectCast(rptMenus.Items(index).FindControl("hdnOptionID2"), HiddenField)

                    If Not IsNothing(hdnOptionPopUp1) AndAlso IsNumeric(hdnOptionPopUp1.Value) Then
                        optionPopupId1 = CInt(hdnOptionPopUp1.Value)
                    End If

                    If Not IsNothing(hdnOptionPopUp2) AndAlso IsNumeric(hdnOptionPopUp2.Value) Then
                        optionPopupId2 = CInt(hdnOptionPopUp2.Value)
                    End If

                    Dim clientScripts As New StringBuilder()

                    Dim blnHasDressing As Boolean = False
                    Dim hdHasDressing As HiddenField = CType(rptMenus.Items(index).FindControl("hdHasDressing"), HiddenField)
                    If Not IsNothing(hdHasDressing) AndAlso Not String.IsNullOrEmpty(hdHasDressing.Value) Then
                        blnHasDressing = hdHasDressing.Value.ToString.ToLower() = "true"
                    End If

                    Dim hdnHasTopping As HiddenField = CType(rptMenus.Items(index).FindControl("hdHasTopping"), HiddenField)
                    Dim blnHasTopping As Boolean = False
                    If Not IsNothing(hdnHasTopping) Then
                        blnHasTopping = hdnHasTopping.Value.ToLower() = "true"
                    End If

                    If Not blnHasDressing Then
                        If blnHasTopping Then
                            'link topping with this item
                            clientScripts.Append("MarkAsNewItemAdding();")
                            clientScripts.Append("PopupToppings('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "','" & optionPopupId2 & "');")
                        Else
                            clientScripts.Append("MarkAsNewItemAdding();")
                            'clientScripts.Append("AddProductToShoppingCart('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "');")
                            ' ID = 101 --> Dressing Linking
                            ' ID = 102 --> Topping Linking
                            If (optionPopupId1 > 0 AndAlso optionPopupId1 <> 101 AndAlso optionPopupId1 <> 102) Then
                                clientScripts.Append("PopupOptions('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "','" & optionPopupId1 & "','" & optionPopupId2 & "');")
                                'clientScripts.Append("$.fancybox({type: 'inline',content: '#ctl00_MainContent_" & optionPopupId1 & "_pnlOption'})")
                            Else
                                clientScripts.Append("AddProductToShoppingCart('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "');")
                            End If
                        End If
                    Else
                        'link dressing with this item
                        'Dim fancyPops = "$.fancybox({type: 'inline',content: '#ctl00_MainContent_" & optionPopupId1 & "_pnlOption'})"

                        clientScripts.Append("MarkAsNewItemAdding();")
                        clientScripts.Append("PopupDressings('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "','" & optionPopupId2 & "', '" & CategoryID & "');")

                    End If



                    vOrderImageButton.Attributes.Add("OnClick", "" & clientScripts.ToString() & ";return false;")
                End If

                ' Check if shop is closed, hide order button
                If Not CheckShopStatus(RemainingTime) Then
                    vOrderImageButton.Visible = False
                End If
            Next
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub

    Protected Sub FillBaseSelection()
        _log.Info("BEGIN")

        Dim vBusiness As New MenuBusinessLogic()
        Try
            'Fill The size against this dropdown if it exists
            Dim vCurrentIndex As Integer = 0
            For vIndex As Integer = 0 To rptMenus.Items.Count - 1

                Dim vSubMenuDropDownList As DropDownList = CType(rptMenus.Items(vIndex).FindControl("SubMenuDropDownList"), DropDownList)
                Dim vBaseSelectionDropDownList As DropDownList = CType(rptMenus.Items(vIndex).FindControl("BaseSelectionDropDownList"), DropDownList)

                'Dim vPriceLabel As Label = CType(MenuItemDataList.Items(vIndex).FindControl("lblItemPrice"), Label)
                Dim vPriceLabel As Label = Nothing
                If IsDeliveryOrder Then
                    vPriceLabel = CType(rptMenus.Items(vIndex).FindControl("lblDeliveryPrice"), Label)
                Else
                    vPriceLabel = CType(rptMenus.Items(vIndex).FindControl("lblCollectionPrice"), Label)
                End If

                Dim vPriceHiddenField As HiddenField = Nothing
                If IsDeliveryOrder Then
                    vPriceHiddenField = CType(rptMenus.Items(vIndex).FindControl("PriceHiddenField"), HiddenField)
                Else
                    vPriceHiddenField = CType(rptMenus.Items(vIndex).FindControl("CollectionPriceHiddenField"), HiddenField)
                End If

                Dim vToppingPriceHiddenField As HiddenField = CType(rptMenus.Items(vIndex).FindControl("hdToppingPrice"), HiddenField)

                If Not vBaseSelectionDropDownList Is Nothing Then
                    If vSubMenuDropDownList.Items.Count > 0 And vBaseSelectionDropDownList.Visible Then

                        Dim vBaseSelections As IEnumerable(Of CsBaseSelection) = vBusiness.GetBaseSelections(vSubMenuDropDownList.SelectedIndex)
                        Dim vEnumerator As IEnumerator(Of CsBaseSelection) = vBaseSelections.GetEnumerator()

                        vBaseSelectionDropDownList.Items.Clear()
                        If vEnumerator.MoveNext() Then
                            vBaseSelectionDropDownList.Items.Add(vEnumerator.Current.Name)

                            While vEnumerator.MoveNext()
                                vBaseSelectionDropDownList.Items.Add(vEnumerator.Current.Name)
                            End While
                            vBaseSelectionDropDownList.Visible = True
                        Else
                            vBaseSelectionDropDownList.Visible = False
                        End If

                        vBaseSelectionDropDownList.SelectedIndex = vCurrentIndex
                        vCurrentIndex = 0
                    Else
                        vBaseSelectionDropDownList.Visible = False
                    End If
                End If

                If vBaseSelectionDropDownList.Items.Count > 0 And vBaseSelectionDropDownList.Visible Then

                    Dim vSubMenuItemID As Decimal = 0
                    Dim vItemNameLabel As Label = CType(rptMenus.Items(vIndex).FindControl("ItemNameLabel"), Label)
                    Dim vOrderImageButton As ImageButton = CType(rptMenus.Items(vIndex).FindControl("ImgAdd2Cart"), ImageButton)
                    Dim vMenuIdHiddenField As HiddenField = CType(rptMenus.Items(vIndex).FindControl("hdMenuId"), HiddenField)
                    Dim vItemName As String = vItemNameLabel.Text

                    vOrderImageButton.ImageUrl = ResolveUrl(EPATheme.Current.Themes.AddToCartImageUrl)

                    If Not vSubMenuDropDownList Is Nothing Then
                        If vSubMenuDropDownList.Items.Count > 0 Then
                            vItemName = vItemName & "+" & vSubMenuDropDownList.SelectedItem.Text
                            vSubMenuItemID = CDec(vSubMenuDropDownList.SelectedValue)
                        End If
                    End If

                    If Not vBaseSelectionDropDownList Is Nothing Then
                        If vBaseSelectionDropDownList.Items.Count > 0 Then
                            vItemName = vItemName & "+" & vBaseSelectionDropDownList.SelectedItem.Text
                            vPriceLabel.Text = vPriceHiddenField.Value
                        End If
                    End If

                    Dim optionPopupId1 As Integer = 0
                    Dim optionPopupId2 As Integer = 0

                    Dim hdnOptionPopUp1 As HiddenField = DirectCast(rptMenus.Items(vIndex).FindControl("hdnOptionID1"), HiddenField)
                    Dim hdnOptionPopUp2 As HiddenField = DirectCast(rptMenus.Items(vIndex).FindControl("hdnOptionID2"), HiddenField)

                    If Not IsNothing(hdnOptionPopUp1) AndAlso IsNumeric(hdnOptionPopUp1.Value) Then
                        optionPopupId1 = CInt(hdnOptionPopUp1.Value)
                    End If

                    If Not IsNothing(hdnOptionPopUp2) AndAlso IsNumeric(hdnOptionPopUp2.Value) Then
                        optionPopupId2 = CInt(hdnOptionPopUp2.Value)
                    End If

                    Dim clientScripts As New StringBuilder()

                    Dim blnHasDressing As Boolean = False
                    Dim hdHasDressing As HiddenField = CType(rptMenus.Items(vIndex).FindControl("hdHasDressing"), HiddenField)
                    If Not IsNothing(hdHasDressing) AndAlso Not String.IsNullOrEmpty(hdHasDressing.Value) Then
                        blnHasDressing = hdHasDressing.Value.ToString.ToLower() = "true"
                    End If

                    Dim hdnHasTopping As HiddenField = CType(rptMenus.Items(vIndex).FindControl("hdHasTopping"), HiddenField)
                    Dim blnHasTopping As Boolean = False
                    If Not IsNothing(hdnHasTopping) Then
                        blnHasTopping = hdnHasTopping.Value.ToLower() = "true"
                    End If

                    If Not blnHasDressing Then
                        If blnHasTopping Then
                            'link topping with this item
                            clientScripts.Append("MarkAsNewItemAdding();")
                            clientScripts.Append("PopupToppings('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "','" & optionPopupId2 & "');")
                        Else
                            clientScripts.Append("MarkAsNewItemAdding();")
                            ' ID = 101 --> Dressing Linking
                            ' ID = 102 --> Topping Linking
                            If (optionPopupId1 > 0 AndAlso optionPopupId1 <> 101 AndAlso optionPopupId1 <> 102) Then
                                clientScripts.Append("PopupOptions('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "','" & optionPopupId1 & "','" & optionPopupId2 & "');")
                            Else
                                clientScripts.Append("AddProductToShoppingCart('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "');")
                            End If
                        End If

                    Else
                        'link dressing with this item
                        clientScripts.Append("MarkAsNewItemAdding();")
                        clientScripts.Append("PopupDressings('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "','" & optionPopupId2 & "', '" & CategoryID & "');")
                    End If

                    vOrderImageButton.Attributes.Add("OnClick", clientScripts.ToString())
                End If
            Next
        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub
#End Region

#Region "MenuItemDataList Events Handling"

    Private Sub rptMenus_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptMenus.ItemCommand
        _log.Info("BEGIN")
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                Dim vItemNameLabel As Label
                Dim vPriceHiddenField As HiddenField

                vItemNameLabel = CType(rptMenus.Items(e.Item.ItemIndex).FindControl("ItemNameLabel"), Label)
                vPriceHiddenField = CType(rptMenus.Items(e.Item.ItemIndex).FindControl("PriceHiddenField"), HiddenField)

                Session("ItemName") = vItemNameLabel.Text
                Session("Price") = vPriceHiddenField.Value

                Dim vSubMenuDropDownList As DropDownList
                vSubMenuDropDownList = CType(rptMenus.Items(e.Item.ItemIndex).FindControl("SubMenuDropDownList"), DropDownList)

                If Not vSubMenuDropDownList Is Nothing Then
                    If vSubMenuDropDownList.Items.Count > 0 Then
                        Session("ItemName") = Session("ItemName") & " + " & vSubMenuDropDownList.SelectedItem.ToString()
                    End If
                End If
            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Private Sub rptMenus_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptMenus.ItemDataBound
        _log.Info("BEGIN")

        Dim vBusiness As New MenuBusinessLogic()
        Try
            Dim hdMenuId As HiddenField = CType(e.Item.FindControl("hdMenuId"), HiddenField)
            Dim vMenuID As Integer = 0
            If Not IsNothing(hdMenuId) AndAlso IsNumeric(hdMenuId.Value) Then
                vMenuID = CInt(hdMenuId.Value)
            End If

            Dim vSubMenuDropDownList As DropDownList = CType(e.Item.FindControl("SubMenuDropDownList"), DropDownList)
            Dim vBaseSelectionDropDownList As DropDownList = CType(e.Item.FindControl("BaseSelectionDropDownList"), DropDownList)
            Dim isBase As HiddenField = CType(e.Item.FindControl("hdHasBase"), HiddenField)
            Dim isTopping As HiddenField = CType(e.Item.FindControl("hdHasTopping"), HiddenField)

            Dim vSubMenuItems As IEnumerable(Of CsSubMenuItem) = vBusiness.GetSubMenuItemsByMenuID(vMenuID)
            vSubMenuDropDownList.DataSource = vSubMenuItems
            vSubMenuDropDownList.DataValueField = "ID"
            vSubMenuDropDownList.DataTextField = "Name"

            vSubMenuDropDownList.DataBind()

            Dim vEnumerator As IEnumerator(Of CsSubMenuItem) = vSubMenuItems.GetEnumerator()
            If vEnumerator.MoveNext() Then
                vSubMenuDropDownList.SelectedIndex = vSubMenuDropDownList.Items.Count - 1
                vSubMenuDropDownList.Visible = True
            Else
                vSubMenuDropDownList.Visible = False
            End If

            If isBase.Value <> "True" Then
                vBaseSelectionDropDownList.Visible = False
            End If

            If isTopping.Value <> "True" Then

                Dim lnkTopping As LinkButton
                lnkTopping = CType(e.Item.FindControl("lnkExtraTopping"), LinkButton)

                If Not lnkTopping Is Nothing Then
                    lnkTopping.Visible = False
                End If
            End If

        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Private Sub rptMenus_PreRender(sender As Object, e As EventArgs) Handles rptMenus.PreRender
        _log.Info("BEGIN")
        Try
            If Not IsPostBack Then
                UpdatePriceInformation()
                FillBaseSelection()
            End If
        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub
#End Region

    Protected Sub SubMenuDropDownList_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        _log.Info("BEGIN")
        UpdatePriceInformation()
        FillBaseSelection()
        _log.Info("END")
    End Sub

    Protected Sub BaseSelectionDropDownList_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        _log.Info("BEGIN")

        Try
            For vIndex As Integer = 0 To rptMenus.Items.Count - 1

                Dim vBaseSelectionDropDownList As DropDownList = CType(rptMenus.Items(vIndex).FindControl("BaseSelectionDropDownList"), DropDownList)
                Dim vPriceHiddenField As HiddenField = CType(rptMenus.Items(vIndex).FindControl("PriceHiddenField"), HiddenField)

                'Dim vPriceLabel As Label = CType(MenuItemDataList.Items(vIndex).FindControl("lblItemPrice"), Label)
                Dim vPriceLabel As Label = Nothing
                If IsDeliveryOrder Then
                    vPriceLabel = CType(rptMenus.Items(vIndex).FindControl("lblDeliveryPrice"), Label)
                Else
                    vPriceLabel = CType(rptMenus.Items(vIndex).FindControl("lblCollectionPrice"), Label)
                End If

                Dim vSubMenuDropDownList As DropDownList = CType(rptMenus.Items(vIndex).FindControl("ddlSubMenu"), DropDownList)
                Dim vToppingPriceHiddenField As HiddenField = CType(rptMenus.Items(vIndex).FindControl("hdToppingPrice"), HiddenField)

                If Not vBaseSelectionDropDownList Is Nothing Then
                    If vBaseSelectionDropDownList.Items.Count > 0 Then

                        Dim vBaseSelectionStr As String = vBaseSelectionDropDownList.SelectedItem.ToString
                        If vBaseSelectionStr.IndexOf("@", StringComparison.Ordinal) > -1 Then
                            'price found for this selection, must be stuffed crust etc
                            vBaseSelectionStr = Trim(vBaseSelectionStr.Substring(vBaseSelectionStr.IndexOf("@", StringComparison.Ordinal) + 2))
                        Else
                            vBaseSelectionStr = "0"
                        End If

                        'here str has got the value to add in
                        Dim vItemPrice As String = "0"
                        Dim vItemNameLabel As Label = CType(rptMenus.Items(vIndex).FindControl("ItemNameLabel"), Label)
                        Dim vOrderImageButton As ImageButton = CType(rptMenus.Items(vIndex).FindControl("ImgAdd2Cart"), ImageButton)
                        Dim vMenuIdHiddenField As HiddenField = CType(rptMenus.Items(vIndex).FindControl("hdMenuId"), HiddenField)
                        Dim vItemName As String = vItemNameLabel.Text

                        vOrderImageButton.ImageUrl = ResolveUrl(EPATheme.Current.Themes.AddToCartImageUrl)

                        Dim vSubMenuItemID As Integer = 0
                        If Not vSubMenuDropDownList Is Nothing Then
                            If vSubMenuDropDownList.Items.Count > 0 Then
                                vItemName = vItemName & "+" & vSubMenuDropDownList.SelectedItem.Text
                                vSubMenuItemID = CInt(vSubMenuDropDownList.SelectedValue)
                            End If
                        End If

                        If Not vBaseSelectionDropDownList Is Nothing Then
                            If vBaseSelectionDropDownList.Items.Count > 0 Then
                                vItemName = vItemName & "+" & vBaseSelectionDropDownList.SelectedItem.Text
                                vItemPrice = FormatNumber(CDbl(vPriceHiddenField.Value) + CDbl(vBaseSelectionStr), 2)
                                vPriceLabel.Text = vItemPrice
                            End If
                        End If

                        vOrderImageButton.Attributes.Add("OnClick", "AddProductToShoppingCart('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "');return false;")

                        Dim lnkTopping As LinkButton
                        lnkTopping = CType(rptMenus.Items(vIndex).FindControl("lnkExtraTopping"), LinkButton)

                        If Not lnkTopping Is Nothing Then
                            If lnkTopping.Visible Then
                                lnkTopping.Attributes.Add("OnClick", "SelectMenuId('" & vMenuIdHiddenField.Value & "','" & vSubMenuItemID & "','" & vToppingPriceHiddenField.Value & "')")
                            End If
                        End If

                    End If
                End If
            Next
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub

    Private Sub BuildOptionPopups()
        _log.Info("BEGIN")

        Try
            Dim bzMenu As New MenuBusinessLogic()
            Dim allMenuOptions As IEnumerable(Of CsMenuOption) = bzMenu.GetAllMenuOptions()
            For Each menuOption As CsMenuOption In allMenuOptions

                'Create a link
                Dim anchor As New HtmlGenericControl("a")
                anchor.ID = menuOption.ID & "_PopupOptionLink"
                anchor.Attributes.Add("href", "#")
                'anchor.Attributes.Add("onclick", "return false;")
                anchor.Attributes.Add("onclick", "SetCurrentOptionId('" & menuOption.ID & "');")
                anchor.Attributes.Add("display", "none")
                anchor.Attributes.Add("visibility", "hidden")
                phOptionPopups.Controls.Add(anchor)

                'Create panel for the option details
                Dim pnlOption As New Panel()
                pnlOption.ID = menuOption.ID & "_pnlOption"
                pnlOption.Style.Add("display", "none")
                pnlOption.Attributes.Add("class", "customPopup")

                Dim divModalPopup As New HtmlGenericControl("div")
                divModalPopup.ID = menuOption.ID & "_div_modal_poup"
                divModalPopup.Attributes.Add("class", "custom-modal-popup")

                Dim divButtons As New HtmlGenericControl("div")
                divButtons.ID = menuOption.ID & "_div_buttons"
                divButtons.Attributes.Add("class", "popup_Buttons")
                divButtons.Style.Add("text-align", "center")

                Dim okButton As New Button()
                okButton.ID = menuOption.ID & "_OkButton"
                okButton.Text = "Done"
                okButton.Attributes.Add("class", "okBtn")
                okButton.Style.Add("width", "100px")
                okButton.OnClientClick = "AddMenuItemWithOptions();"
                divButtons.Controls.Add(okButton)

                Dim cancelButton As New HtmlInputButton()
                cancelButton.ID = menuOption.ID & "_CancelButton"
                cancelButton.Attributes.Add("class", "cancelBtn")
                cancelButton.Style.Add("margin-left", "10px")
                cancelButton.Style.Add("width", "100px")
                cancelButton.Value = "Close"
                divButtons.Controls.Add(cancelButton)

                Dim divHeader As New HtmlGenericControl("div")
                divHeader.ID = menuOption.ID & "_div_header"
                divHeader.Attributes.Add("class", "popup-title")

                Dim divHeaderLeft As New HtmlGenericControl("div")
                divHeaderLeft.ID = menuOption.ID & "_div_header_left"
                divHeaderLeft.InnerText = menuOption.Name
                divHeaderLeft.Attributes.Add("class", "TitlebarLeft")

                Dim divHeaderRight As New HtmlGenericControl("div")
                divHeaderRight.ID = menuOption.ID & "_div_header_right"
                divHeaderRight.Attributes.Add("onclick", "$get('" & cancelButton.ID & "').click();")
                divHeaderRight.Attributes.Add("class", "TitlebarRight")

                divHeader.Controls.Add(divHeaderLeft)
                divHeader.Controls.Add(divHeaderRight)

                Dim divBody As New HtmlGenericControl("div")
                divBody.ID = menuOption.ID & "_div_body"
                divBody.Attributes.Add("class", "popup-body")

                Dim vDataList As New DataList()
                vDataList.ID = menuOption.ID & "_dlMenuOption"
                vDataList.Attributes.Add("class", "table-option-details")

                Dim optionDetails As IEnumerable(Of CsOptionDetail) = bzMenu.GetOptionDetails(menuOption.ID)
                vDataList.ItemTemplate = New MyTemplate(ListItemType.Item)

                'AddHandler vDataList.ItemDataBound, AddressOf MenuOptionDataList_ItemDataBound

                vDataList.DataSource = optionDetails
                vDataList.DataBind()

                divBody.Controls.Add(vDataList)

                Dim hdnItemsAllowed As New HtmlInputHidden()
                hdnItemsAllowed.ID = menuOption.ID & "_hdnItemsAllowed"
                hdnItemsAllowed.Value = menuOption.ItemsAllowed.ToString()

                divBody.Controls.Add(hdnItemsAllowed)

                divModalPopup.Controls.Add(divHeader)
                divModalPopup.Controls.Add(divBody)
                divModalPopup.Controls.Add(divButtons)
                pnlOption.Controls.Add(divModalPopup)

                phOptionPopups.Controls.Add(pnlOption)

                'Create extender to open the panel as a popup
                Dim popupExtender As New ModalPopupExtender() With
                    {
                        .ID = menuOption.ID & "_ModalPopupExtender",
                        .OkControlID = okButton.ID,
                        .CancelControlID = cancelButton.ID,
                        .TargetControlID = anchor.ID,
                        .PopupControlID = pnlOption.ID,
                        .PopupDragHandleControlID = divHeader.ID,
                        .BackgroundCssClass = "ModalPopupBG"
                    }

                phOptionPopups.Controls.Add(popupExtender)
            Next
        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub
End Class

Public Class MyTemplate
    Implements ITemplate

    Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

    Private ReadOnly _itemType As ListItemType

    Public Sub New(vItemType As ListItemType)
        _log.Info("BEGIN")
        _itemType = vItemType
        _log.Info("END")
    End Sub

    Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
        _log.Info("BEGIN")

        Try
            Dim contentTable As New HtmlGenericControl("table")
            contentTable.Style.Add("width", "100%")
            Dim innerControlsContainer As New Literal
            Select Case _itemType
                Case ListItemType.Header
                    Exit Select
                Case ListItemType.Item
                    AddHandler innerControlsContainer.DataBinding, AddressOf InnerControl_DataBinding
                    Exit Select
                Case ListItemType.Footer
                    Exit Select
            End Select
            contentTable.Controls.Add(innerControlsContainer)
            container.Controls.Add(contentTable)
        Catch ex As Exception
            _log.Error(ex)
        End Try
        _log.Info("END")
    End Sub

    Private Sub InnerControl_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        _log.Info("BEGIN")

        If Not (TypeOf sender Is Literal) Then
            Return
        End If

        Try
            Dim lc As Literal = DirectCast(sender, Literal)
            Dim container As DataListItem = DirectCast(lc.NamingContainer, DataListItem)

            Dim htmlText As New StringBuilder()
            htmlText.AppendLine("<tr>")
            htmlText.AppendLine("   <td style='text-align:left;'>")
            htmlText.AppendLine("       <div class=""checkbox"">")
            htmlText.AppendLine("           <label style=""font-weight: bold;"">")
            htmlText.AppendLine("               <input id='checkbox_" & CStr(DataBinder.Eval(container.DataItem, "ID")) & "' " & "type=""checkbox"" onchange=""MarkOptionIdToAdd('" & CStr(DataBinder.Eval(container.DataItem, "ID")) & "', this);"" />" & " " & CStr(DataBinder.Eval(container.DataItem, "Name")))
            htmlText.AppendLine("           </label>")
            htmlText.AppendLine("       <div>")
            'htmlText.AppendLine("           <input id='checkbox_" & CStr(DataBinder.Eval(container.DataItem, "ID")) & "' " & "type=""checkbox"" class=""on_off"" onchange=""MarkOptionIdToAdd('" & CStr(DataBinder.Eval(container.DataItem, "ID")) & "', this);"" data-label=""" & CStr(DataBinder.Eval(container.DataItem, "Name")) & """ />" & "")
            htmlText.AppendLine("   </td>")
            htmlText.AppendLine("   <td style='width:20%;'>")
            htmlText.AppendLine("       <label style=""font-weight: bold;"">")
            htmlText.AppendLine("           £" & CStr(DataBinder.Eval(container.DataItem, "UnitPrice")).Trim())
            htmlText.AppendLine("       </label>")
            htmlText.AppendLine("   </td>")
            htmlText.AppendLine("</tr>")
            lc.Text += htmlText.ToString()

            'lc.Text += "<div style='background-color:#FFFFCC'><A href=" & CStr(DataBinder.Eval(container.DataItem, "Name")) & ">" & CStr(DataBinder.Eval(container.DataItem, "Name")) & " </a></div> "
        Catch ex As Exception
            _log.Error(ex)
        End Try

        _log.Info("END")
    End Sub
End Class
