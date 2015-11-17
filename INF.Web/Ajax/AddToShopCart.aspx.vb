Imports INF.Web.UI.Shopping
Imports INF.Web.UI.Utils
Imports INF.Web.UI.Logging.Log4Net

Namespace Ajax
    Partial Class AddToShopCart
        Inherits System.Web.UI.Page

        Private Shared ReadOnly _log As Log4NetLogger = New Log4NetLogger()

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            _log.Info("BEGIN")
            Try
                Dim szCartId As String = WebUtil.GetParameterValueAsString(Page.Request, "CartId")

                Dim dealId As Integer = WebUtil.GetParameterValueAsInteger(Page.Request, "DealId")
                Dim menuItemId As String = WebUtil.GetParameterValueAsString(Page.Request, "MenuItemId")
                Dim subMenuItemId As String = WebUtil.GetParameterValueAsString(Page.Request, "SubMenuItemId")

                'In the case adding comes from deal, the memu_id will be a combination of Ids, saperated by semi-colon
                Dim intMenuIds() As Integer = New Integer() {}
                Dim intSubMenuIds() As Integer = New Integer() {}

                If Not String.IsNullOrEmpty(menuItemId) Then
                    Dim idStr() As String = menuItemId.Split(";")
                    intMenuIds = (From sId In idStr
                                  Where Not String.IsNullOrEmpty(sId) AndAlso IsNumeric(sId)
                                  Select CInt(sId)).ToArray()
                End If

                If Not String.IsNullOrEmpty(subMenuItemId) Then
                    Dim idStr() As String = subMenuItemId.Split(";")
                    intSubMenuIds = (From sId In idStr
                                  Where Not String.IsNullOrEmpty(sId) AndAlso IsNumeric(sId)
                                  Select CInt(sId)).ToArray()
                End If

                Dim inclToppingIds As String = WebUtil.GetParameterValueAsString(Page.Request, "InclToppingIds")
                Dim exclToppingIds As String = WebUtil.GetParameterValueAsString(Page.Request, "ExclToppingIds")
                Dim leftToppingIds As String = WebUtil.GetParameterValueAsString(Page.Request, "LeftToppingIds")
                Dim rightToppingIds As String = WebUtil.GetParameterValueAsString(Page.Request, "RightToppingIds")

                Dim dressingIds As String = WebUtil.GetParameterValueAsString(Page.Request, "DressingIds")
                Dim optionIds As String = WebUtil.GetParameterValueAsString(Page.Request, "optionIds")

                '*****************************************************************************
                ' For the case of order comming from DEALS page. It might consist of many 
                ' menu item with their options
                '*****************************************************************************
                If (dealId > 0) Then
                    Dim arrInclToppingIds() As String = ExtractOptionsIds(inclToppingIds)
                    Dim arrDressingIds() As String = ExtractOptionsIds(dressingIds)
                    Dim arrOptionsIds() As String = ExtractOptionsIds(optionIds)

                    Dim basket As BxShoppingCart = BxShoppingCart.GetShoppingCart()

                    ' Check to see if there is already an item created for the deal/menuitem/submenuitem
                    szCartId = basket.HasExistedItem(dealId, intMenuIds, intSubMenuIds, arrInclToppingIds, arrDressingIds, arrOptionsIds)
                    If Not String.IsNullOrWhiteSpace(szCartId) Then
                        basket.AddItem(szCartId)
                        Response.Write(szCartId)
                        Exit Sub
                    End If

                    For index As Integer = 0 To intMenuIds.Length - 1
                        Dim menuId As Integer = intMenuIds(index)

                        Dim subMenuId As Integer = 0
                        If intSubMenuIds.Length > index Then
                            subMenuId = intSubMenuIds(index)
                        End If

                        ' Check if there is any topping adding to the item
                        Dim intToppingIDs() As Integer = New Integer() {}
                        If Not IsNothing(arrInclToppingIds) AndAlso arrInclToppingIds.Length > index Then
                            If Not String.IsNullOrEmpty(arrInclToppingIds(index)) Then
                                Dim idStr() As String = arrInclToppingIds(index).Split(";")
                                intToppingIDs = (From sId In idStr
                                                 Where Not String.IsNullOrEmpty(sId) AndAlso IsNumeric(sId)
                                                 Select CInt(sId)).ToArray()
                            End If
                        End If

                        ' Check if there is any dressing adding to the item
                        Dim intDressingIDs() As Integer = New Integer() {}
                        If Not IsNothing(arrDressingIds) AndAlso arrDressingIds.Length > index Then
                            If Not String.IsNullOrEmpty(arrDressingIds(index)) Then
                                Dim idStr() As String = arrDressingIds(index).Split(";"c)
                                intDressingIDs = (From sID In idStr
                                                 Where Not String.IsNullOrEmpty(sID) AndAlso IsNumeric(sID)
                                                 Select CInt(sID)).ToArray()
                            End If
                        End If

                        ' Check if there is any options adding to the item
                        Dim intOptionIds() As Integer = New Integer() {}
                        If Not IsNothing(arrOptionsIds) AndAlso arrOptionsIds.Length > index Then
                            If Not String.IsNullOrEmpty(arrOptionsIds(index)) Then
                                Dim idStr() As String = arrOptionsIds(index).Split(";"c)
                                intOptionIds = (From sId In idStr
                                               Where Not String.IsNullOrEmpty(sId) AndAlso IsNumeric(sId)
                                               Select CInt(sId)).ToArray()
                            End If
                        End If

                        szCartId = basket.AddItem(szCartId, dealId, menuId, subMenuId)

                        '*************************************************************************
                        ' Addding topping items
                        '*************************************************************************
                        For i As Integer = 0 To intToppingIDs.Length - 1
                            If intToppingIDs(i) > 0 Then basket.AddToppingOnMenuItem(szCartId, menuId, subMenuId, intToppingIDs(i), "")
                        Next

                        '*************************************************************************
                        ' Addding dressing items
                        '*************************************************************************
                        For j As Integer = 0 To intDressingIDs.Length - 1
                            If intDressingIDs(j) > 0 Then basket.AddDressingOnMenuItem(szCartId, menuId, intDressingIDs(j))
                        Next

                        '*************************************************************************
                        ' Addding option items
                        '*************************************************************************
                        For k As Integer = 0 To intOptionIds.Length - 1
                            If intOptionIds(k) > 0 Then basket.AddItemWithOptions(szCartId, menuId, intOptionIds(k))
                        Next
                    Next
                    Response.Write(szCartId)
                Else
                    '*****************************************************************************
                    ' For the case of order comming from MENU page
                    '*****************************************************************************

                    Dim menuId As Integer = intMenuIds(0)
                    Dim subMenuId As Integer = intSubMenuIds(0)

                    Dim intToppingIDs() As Integer
                    If Not String.IsNullOrEmpty(inclToppingIds) Then
                        Dim idStr() As String = inclToppingIds.Split(";")
                        intToppingIDs = (From sId In idStr
                                         Where Not String.IsNullOrEmpty(sId) AndAlso IsNumeric(sId)
                                         Select CInt(sId)).ToArray()
                    End If


                    Dim intDressingIDs() As Integer
                    If Not String.IsNullOrEmpty(dressingIds) Then
                        Dim idStr() As String = dressingIds.Split(";"c)
                        intDressingIDs = (From sID In idStr
                                         Where Not String.IsNullOrEmpty(sID) AndAlso IsNumeric(sID)
                                         Select CInt(sID)).ToArray()
                    End If

                    Dim intOptionIds() As Integer
                    If Not String.IsNullOrEmpty(optionIds) Then
                        Dim idStr() As String = optionIds.Split(";"c)
                        intOptionIds = (From sId In idStr
                                       Where Not String.IsNullOrEmpty(sId) AndAlso IsNumeric(sId)
                                       Select CInt(sId)).ToArray()
                    End If

                    Dim basket As BxShoppingCart = BxShoppingCart.GetShoppingCart()
                    ' Check to see if there is already an item created for the deal/menuitem/submenuitem
                    If (String.IsNullOrWhiteSpace(szCartId)) Then
                        szCartId = basket.HasExistedItem(dealId, menuId, subMenuId, intToppingIDs, intDressingIDs, intOptionIds)

                        If String.IsNullOrWhiteSpace(szCartId) Then
                            szCartId = basket.AddItem(dealId, menuId, subMenuId)
                        Else
                            basket.AddItem(szCartId)
                            Response.Write(szCartId)
                            Exit Sub
                        End If
                    End If

                    '*************************************************************************
                    ' Addding topping items
                    '*************************************************************************
                    If Not String.IsNullOrWhiteSpace(inclToppingIds) Then
                        Dim ids = inclToppingIds.Split(";"c)
                        For Each idStr As String In ids
                            If Not String.IsNullOrWhiteSpace(idStr) AndAlso IsNumeric(idStr) Then
                                Dim toppingId As Integer = CInt(idStr)
                                'basket.AddSubItem(szCartId, 0, toppingId)

                                basket.AddToppingOnMenuItem(szCartId, menuId, subMenuId, toppingId, "")
                            End If
                        Next
                    End If

                    If Not String.IsNullOrWhiteSpace(exclToppingIds) Then
                        Dim ids = exclToppingIds.Split(";"c)
                        For Each idStr As String In ids
                            If Not String.IsNullOrWhiteSpace(idStr) AndAlso IsNumeric(idStr) Then
                                Dim toppingId As Integer = CInt("-" + idStr)
                                'basket.AddSubItem(szCartId, 0, toppingId)
                                basket.AddToppingOnMenuItem(szCartId, menuId, subMenuId, toppingId, "")
                            End If
                        Next
                    End If

                    If Not String.IsNullOrWhiteSpace(leftToppingIds) Then
                        Dim ids = leftToppingIds.Split(";"c)
                        For Each idStr As String In ids
                            If Not String.IsNullOrWhiteSpace(idStr) AndAlso IsNumeric(idStr) Then
                                Dim toppingId As Integer = CInt(idStr)
                                'basket.AddSubItem(szCartId, 0, toppingId, "left")
                                basket.AddToppingOnMenuItem(szCartId, menuId, subMenuId, toppingId, "left")
                            End If
                        Next
                    End If

                    If Not String.IsNullOrWhiteSpace(rightToppingIds) Then
                        Dim ids = rightToppingIds.Split(";"c)
                        For Each idStr As String In ids
                            If Not String.IsNullOrWhiteSpace(idStr) AndAlso IsNumeric(idStr) Then
                                Dim toppingId As Integer = CInt(idStr)
                                'basket.AddSubItem(szCartId, 0, toppingId, "right")
                                basket.AddToppingOnMenuItem(szCartId, menuId, subMenuId, toppingId, "right")
                            End If
                        Next
                    End If

                    '*************************************************************************
                    ' Addding dressing items
                    '*************************************************************************
                    If Not String.IsNullOrWhiteSpace(dressingIds) Then
                        Dim ids() As String = dressingIds.Split(";"c)
                        Dim lstDressingIds As List(Of Integer) = (From idStr In ids _
                                Where Not String.IsNullOrWhiteSpace(idStr) AndAlso IsNumeric(idStr) _
                                Select CInt(idStr)).ToList()

                        For Each drID As Integer In lstDressingIds
                            If drID > 0 Then basket.AddDressingOnMenuItem(szCartId, menuId, drID)
                        Next
                    End If

                    '*************************************************************************
                    ' Addding option items
                    '*************************************************************************
                    If Not String.IsNullOrWhiteSpace(optionIds) Then
                        Dim ids = optionIds.Split(";"c)
                        For Each idStr As String In ids
                            If Not String.IsNullOrWhiteSpace(idStr) AndAlso IsNumeric(idStr) Then
                                Dim opptionId As Integer = CInt(idStr)
                                basket.AddItemWithOptions(szCartId, menuId, opptionId)
                            End If
                        Next
                    End If
                    Response.Write(szCartId)
                End If
            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try

            _log.Info("END")
        End Sub

        Private Function ExtractOptionsIds(vInput As String) As String()
            _log.Info("BEGIN")

            Try
                If IsNothing(vInput) OrElse String.IsNullOrEmpty(vInput) Then Return Nothing

                Dim idCombinationList As IList(Of String) = New List(Of String)()

                Dim openBracketPos As Integer = -1
                Dim closeBracketPos As Integer = -1
                Dim leftString As String = vInput

                Do
                    openBracketPos = leftString.IndexOf("{", StringComparison.Ordinal)
                    closeBracketPos = leftString.IndexOf("}", StringComparison.Ordinal)

                    If openBracketPos > -1 AndAlso closeBracketPos > -1 AndAlso closeBracketPos > openBracketPos Then
                        Dim elements As String = leftString.Substring(openBracketPos + 1, closeBracketPos - openBracketPos - 1)
                        idCombinationList.Add(elements)
                        leftString = leftString.Substring(closeBracketPos + 1)
                    Else
                        Exit Do
                    End If
                Loop Until (leftString.Length = 0)

                Return idCombinationList.ToArray()
            Catch ex As Exception
                _log.Error(ex)
                Response.Write("Error")
            End Try

            _log.Info("END")
            Return Nothing
        End Function
    End Class
End Namespace