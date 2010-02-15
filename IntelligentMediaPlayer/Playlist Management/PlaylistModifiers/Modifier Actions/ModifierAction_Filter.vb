
Partial Public Class PlaylistManager

    Public Class ModifierAction_Filter
        Implements IModifierAction

        Private Const myName As String = "Filter"

        Public Sub New()

        End Sub

        Public Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, ByRef attributeLookupArray() As Dictionary(Of String, String)) Implements IModifierAction.ModifyPlaylist


            Dim quickMediaLookup As Dictionary(Of String, Media) = New Dictionary(Of String, Media) 'for proper, fast subtraction

            For Each attributeLookup As Dictionary(Of String, String) In attributeLookupArray
                Dim query As New MediaCollection.Query

                For Each attributeName As String In attributeLookup.Keys().ToArray()
                    query.addCondition(attributeName, "Equals", attributeLookup.Item(attributeName))
                Next

                Dim result As Playlist = mediaCollection.GetPlaylistByQuery(query)


                For y As Integer = 0 To result.count - 1 Step 1
                    If (y >= currentPlaylist.count) Then 'need to check since we're potentially removing items.
                        Exit For
                    End If

                    Dim item As Media = result.Item(y)
                    Dim key As String = GenerateMediaHashKey(item)

                    If (Not (quickMediaLookup.ContainsKey(key))) Then
                        quickMediaLookup.Add(key, item)
                    End If
                Next

            Next

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                If (index >= currentPlaylist.count) Then
                    Exit For
                End If

                Dim mediaItem As Media = currentPlaylist.Item(index)
                Dim key As String = GenerateMediaHashKey(mediaItem)

                If (Not quickMediaLookup.ContainsKey(key)) Then
                    currentPlaylist.removeItem(mediaItem)
                    index -= 1
                Else
                    quickMediaLookup.Remove(key)
                End If

            Next
        End Sub

        Public Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, ByRef modifyingPlaylist As Playlist) Implements IModifierAction.ModifyPlaylist
            Dim quickMediaLookup As Dictionary(Of String, Media) = New Dictionary(Of String, Media) 'for proper, fast subtraction

            For index As Integer = 0 To modifyingPlaylist.count - 1 Step 1
                Dim mediaItem As Media = modifyingPlaylist.Item(index)
                Dim key As String = GenerateMediaHashKey(mediaItem)
                If (Not quickMediaLookup.ContainsKey(key)) Then
                    quickMediaLookup.Add(key, mediaItem)
                End If
            Next

            For y As Integer = 0 To currentPlaylist.count - 1 Step 1
                If (y >= currentPlaylist.count) Then 'need to check since we're potentially removing items.
                    Exit For
                End If

                Dim item As Media = currentPlaylist.Item(y)
                Dim key As String = GenerateMediaHashKey(item)

                If (Not quickMediaLookup.ContainsKey(key)) Then
                    currentPlaylist.removeItem(item)
                    y -= 1
                End If
            Next
        End Sub

        Public ReadOnly Property Name As String Implements IModifierAction.Name
            Get
                Return myName
            End Get
        End Property

    End Class

End Class
