Imports AxWMPLib
Partial Public Class PlaylistManager

    Public Interface IModifierAction
        Sub ModifyPlaylist(ByRef player As AxWindowsMediaPlayer, ByRef attributeLookup As Dictionary(Of String, String))
        ReadOnly Property Name() As String
    End Interface

End Class
