Imports AxWMPLib
Partial Public Class PlaylistManager

    Public Interface IModifierAction
        Sub ModifyPlaylist(ByRef player As AxWindowsMediaPlayer, ByRef query As WMPLib.IWMPQuery)
        ReadOnly Property Name() As String
    End Interface

End Class
