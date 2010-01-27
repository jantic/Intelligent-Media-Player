Imports AxWMPLib
Imports WMPLib

Partial Public Class PlaylistManager

    Public Class ModifierAction_Filter
        Implements IModifierAction

        Private Const myName As String = "Filter"

        Public Sub New()

        End Sub

        Public Sub ModifyPlaylist(ByRef player As AxWMPLib.AxWindowsMediaPlayer, ByRef attributeLookup As Dictionary(Of String, String)) Implements IModifierAction.ModifyPlaylist
            Dim mc As WMPLib.IWMPMediaCollection2 = player.mediaCollection
            Dim query As WMPLib.IWMPQuery = mc.createQuery()

            For Each attributeName As String In attributeLookup.Keys().ToArray()
                query.addCondition(attributeName, "Equals", attributeLookup.Item(attributeName))
            Next

            Dim result As IWMPPlaylist = mc.getPlaylistByQuery(query, "audio", "", False)
            Dim quickMediaLookup As Dictionary(Of String, IWMPMedia) = New Dictionary(Of String, IWMPMedia) 'for proper, fast subtraction

            For y As Integer = 0 To player.currentPlaylist.count - 1 Step 1
                If (y >= player.currentPlaylist.count) Then 'need to check since we're potentially removing items.
                    Exit For
                End If

                Dim item As IWMPMedia = player.currentPlaylist.Item(y)
                Dim key As String = GenerateMediaHashKey(item)

                If (Not (quickMediaLookup.ContainsKey(key))) Then
                    quickMediaLookup.Add(key, item)
                Else
                    player.currentPlaylist.removeItem(item)
                    y -= 1
                End If
            Next

            player.currentPlaylist.clear()

            For index As Integer = 0 To result.count - 1 Step 1
                Dim mediaItem As IWMPMedia = result.Item(index)
                Dim key As String = GenerateMediaHashKey(mediaItem)

                If (quickMediaLookup.ContainsKey(key)) Then
                    player.currentPlaylist.appendItem(quickMediaLookup(GenerateMediaHashKey(mediaItem)))
                    quickMediaLookup.Remove(key)
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
