Imports AxWMPLib
Imports WMPLib

Partial Public Class PlaylistManager

    Public Class ModifierAction_Add
        Implements IModifierAction

        Private Const myName As String = "Add"

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
            'this makes subtraction a O(N) operation as opposed to an O(N2) operation

            For index As Integer = 0 To result.count - 1 Step 1
                Dim mediaItem As IWMPMedia = result.Item(index)
                player.currentPlaylist.appendItem(result.Item(index))
            Next
        End Sub

        Public ReadOnly Property Name As String Implements IModifierAction.Name
            Get
                Return myName
            End Get
        End Property

    End Class

End Class