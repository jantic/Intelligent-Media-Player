Imports WMPLib
Imports AxWMPLib


Partial Public Class MediaCollection
    Public Class Query
        Private myConditions As New ArrayList

        Public Sub New()

        End Sub

        Public Sub addCondition(ByVal bstrAttribute As String, ByVal bstrOperator As String, ByVal bstrValue As String)
            myConditions.Add(New Condition(bstrAttribute, bstrOperator, bstrValue))
        End Sub

        Public Function ExtractMatchingPlaylist(ByRef mc As MediaCollection) As Playlist
            Dim matchesLookup As Dictionary(Of String, ArrayList) = Nothing
            Dim resultArrayList As ArrayList = Nothing

            For index As Integer = 0 To myConditions.Count - 1 Step 1
                If (matchesLookup Is Nothing And mc.myAttributeLookup.ContainsKey(DirectCast(myConditions(index), Condition).AttributeName)) Then
                    matchesLookup = mc.myAttributeLookup.Item(DirectCast(myConditions(index), Condition).AttributeName)
                End If
                Dim attributeNameNext As String = Nothing
                If (index < myConditions.Count - 1) Then
                    attributeNameNext = DirectCast(myConditions(index + 1), Condition).AttributeName.ToLower.Trim
                    Dim nextMatchesLookup As New Dictionary(Of String, ArrayList)
                    Dim results As ArrayList = Nothing

                    If (matchesLookup.ContainsKey(DirectCast(myConditions(index), Condition).Value.ToLower.Trim)) Then
                        results = matchesLookup.Item(DirectCast(myConditions(index), Condition).Value.ToLower.Trim)
                    End If

                    If (Not results Is Nothing) Then
                        For Each mediaItem As Media In results
                            If (Not nextMatchesLookup.ContainsKey(mediaItem.getItemInfo(attributeNameNext).ToLower.Trim)) Then
                                nextMatchesLookup.Add(mediaItem.getItemInfo(attributeNameNext).ToLower.Trim, New ArrayList)
                            End If

                            nextMatchesLookup.Item(mediaItem.getItemInfo(attributeNameNext).ToLower.Trim).Add(mediaItem)
                        Next
                        matchesLookup = nextMatchesLookup
                    Else
                        matchesLookup = Nothing
                        Exit For
                    End If
                Else
                    If (matchesLookup.ContainsKey(DirectCast(myConditions(index), Condition).Value.ToLower.Trim)) Then
                        resultArrayList = matchesLookup.Item(DirectCast(myConditions(index), Condition).Value.ToLower.Trim)
                    End If
                End If
            Next

            If (Not resultArrayList Is Nothing) Then
                Return New Playlist(resultArrayList)
            Else
                Return New Playlist()
            End If
        End Function

        Private Class Condition
            Private myAttribute As String
            Private myOperation As Operation
            Private myValue As String
            Public Enum Operation
                Equals
                NotEqual
            End Enum

            Public Sub New(ByVal attribute As String, ByVal operatorName As String, ByVal value As String)
                myOperation = TranslateToOperation(operatorName)
                myAttribute = attribute.Trim.ToLower
                myValue = value.Trim.ToLower
            End Sub

            Private Function TranslateToOperation(ByVal operatorName As String) As Operation
                For Each value As Operation In System.Enum.GetValues(GetType(Operation))
                    If (value.ToString().Trim.ToLower = operatorName.Trim.ToLower) Then Return value
                Next

                Return Nothing
            End Function

            Public ReadOnly Property AttributeName As String
                Get
                    Return myAttribute
                End Get
            End Property

            Public ReadOnly Property Op As Operation
                Get
                    Return myOperation
                End Get
            End Property

            Public ReadOnly Property Value As String
                Get
                    Return myValue
                End Get
            End Property
        End Class
    End Class
End Class