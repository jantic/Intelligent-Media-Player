
Partial Public Class PlaylistManager

    Public Class ModifierAction_Add
        Implements IModifierAction

        Private Const myName As String = "Add"

        Public Sub New()

        End Sub

        Public Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, _
                ByRef attributeLookupArray() As Dictionary(Of String, String)) Implements IModifierAction.ModifyPlaylist

            For Each attributeLookup As Dictionary(Of String, String) In attributeLookupArray
                Dim query As New MediaCollection.Query

                For Each attributeName As String In attributeLookup.Keys().ToArray()
                    query.addCondition(attributeName, "Equals", attributeLookup.Item(attributeName))
                Next

                Dim result As Playlist = mediaCollection.GetPlaylistByQuery(query)
                Dim quickMediaLookup As Dictionary(Of String, Media) = New Dictionary(Of String, Media) 'for proper, fast subtraction
                'this makes subtraction a O(N) operation as opposed to an O(N2) operation

                For index As Integer = 0 To result.count - 1 Step 1
                    Dim mediaItem As Media = result.Item(index)
                    currentPlaylist.appendItem(result.Item(index))
                Next
            Next

        End Sub


        Public Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, _
        ByRef modifyingPlaylist As Playlist) Implements IModifierAction.ModifyPlaylist
            For index As Integer = 0 To modifyingPlaylist.count - 1 Step 1
                Dim mediaItem As Media = modifyingPlaylist.Item(index)
                currentPlaylist.appendItem(mediaItem)
            Next
        End Sub

        Public ReadOnly Property Name As String Implements IModifierAction.Name
            Get
                Return myName
            End Get
        End Property

    End Class

End Class