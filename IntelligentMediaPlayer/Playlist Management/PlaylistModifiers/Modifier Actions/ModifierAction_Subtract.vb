
Imports System.Threading
Imports System.Threading.Tasks

Partial Public Class PlaylistManager

    Public Class ModifierAction_Subtract
        Implements IModifierAction

        Private Const myName As String = "Subtract"

        Public Sub New()

        End Sub

        Public Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, ByRef attributeLookupArray() As Dictionary(Of String, String)) Implements IModifierAction.ModifyPlaylist

            For Each attributeLookup As Dictionary(Of String, String) In attributeLookupArray
                Dim query As New MediaCollection.Query

                For Each attributeName As String In attributeLookup.Keys().ToArray()
                    query.addCondition(attributeName, "Equals", attributeLookup.Item(attributeName))
                Next

                Dim result As Playlist = mediaCollection.GetPlaylistByQuery(query)


                Dim resultMediaLookup As New Dictionary(Of String, Media)

                For x As Integer = 0 To result.count - 1 Step 1
                    Dim key As String = GenerateMediaHashKey(result.Item(x))
                    If (Not resultMediaLookup.ContainsKey(key)) Then
                        resultMediaLookup.Add(key, result.Item(x))
                    End If
                Next

                ' Dim quickMediaLookup As Dictionary(Of String, IWMPMedia) = New Dictionary(Of String, IWMPMedia) 'for proper, fast subtraction

                For y As Integer = 0 To currentPlaylist.count - 1 Step 1
                    If (y >= currentPlaylist.count) Then
                        Exit For
                    End If

                    Dim currentItem As Media = currentPlaylist.Item(y)

                    If (resultMediaLookup.ContainsKey(GenerateMediaHashKey(currentItem))) Then
                        currentPlaylist.removeItem(currentItem)
                        y -= 1
                    End If
                Next

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

                If (Not (quickMediaLookup.ContainsKey(key))) Then
                    quickMediaLookup.Add(key, item)
                Else
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