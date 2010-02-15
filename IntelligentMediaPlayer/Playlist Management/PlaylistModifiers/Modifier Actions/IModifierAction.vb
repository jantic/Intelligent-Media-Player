Partial Public Class PlaylistManager

    Public Interface IModifierAction
        Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, ByRef attributeLookupArray() As Dictionary(Of String, String))
        Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, ByRef modifyingPlaylist As Playlist)
        ReadOnly Property Name() As String
    End Interface

End Class
