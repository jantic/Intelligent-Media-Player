Imports System.IO
Imports System.Xml
Imports System.Xml.XPath


Partial Public Class PlaylistManager
    Public Enum ModifierType
        WMPAttribute
        LastFM
        Meta
    End Enum


    Private myMetaModifier As MetaPlaylistModifier
    Private myModifiersDirectory As String
    Private myLiasons As PlaylistModifierUILiason()


    Public Sub New(ByVal modifiersDirectory As String)
        myModifiersDirectory = modifiersDirectory
        myMetaModifier = New MetaPlaylistModifier(New PlaylistModifierUILiason(myModifiersDirectory))
        LoadModifierLiasons(myModifiersDirectory)
    End Sub

    Public Sub GeneratePlaylist(ByRef player As MusicPlayer)
        InitializePlaylistForModification(player)

        If (myMetaModifier.NumberOfComponentModifiers > 0) Then
            myMetaModifier.ModifyPlaylist(player.currentPlaylist, player.MediaLibrary, True)
        End If

        'Only go through the trouble if current playlist is a reasonable size (<10000).  Otherwise, this takes way too long.
        If (player.currentPlaylist.count < 10000) Then
            RemoveDuplicates(player)
        End If
    End Sub

    Public Sub SaveModiferSequenceAsMetaModifier(ByVal path As String)
        myMetaModifier.SaveAs(path)
        LoadModifierLiasons(myModifiersDirectory) 'to update view
    End Sub

    Public ReadOnly Property ModifiersDirectory As String
        Get
            Return myModifiersDirectory
        End Get
    End Property

    Private Sub InitializePlaylistForModification(ByRef player As MusicPlayer)
        If (myMetaModifier.NumberOfComponentModifiers > 0) Then
            If (myMetaModifier.Liason.ModifierAction.Name = "Add") Then
                player.currentPlaylist.clear()
            ElseIf ((myMetaModifier.Liason.ModifierAction.Name = "Subtract" Or (myMetaModifier.Liason.ModifierAction.Name = "Filter") And _
                     player.currentPlaylist.count = 0)) Then
                player.currentPlaylist = New Playlist(MediaCollection.GetMediaCollection.AllMediaItems)
            End If
        Else
            player.currentPlaylist = New Playlist(MediaCollection.GetMediaCollection.AllMediaItems)
        End If
    End Sub

    Private Sub RemoveDuplicates(ByRef player As MusicPlayer)

        Dim nonDuplicatedList As Dictionary(Of String, Media) = New Dictionary(Of String, Media)

        For x As Integer = 0 To player.currentPlaylist.count - 1 Step 1
            Dim key As String = PlaylistManager.GenerateMediaHashKey(player.currentPlaylist.Item(x))

            If (Not (nonDuplicatedList.ContainsKey(key))) Then
                nonDuplicatedList.Add(key, player.currentPlaylist.Item(x))
            End If
        Next
        player.currentPlaylist = New Playlist(nonDuplicatedList.Values.ToArray())
    End Sub

    Public Sub AddWorkingModifier(ByVal liason As PlaylistModifierUILiason)
        If (File.Exists(liason.FilePath)) Then
            myMetaModifier.AddComponentModifier(liason)
        End If
    End Sub

    Public Sub RemoveWorkingModifier(ByVal index As Integer)
        myMetaModifier.RemoveComponentModifier(index)
    End Sub

    Public Function GetWorkingModifiers() As PlaylistModifierUILiason()
        Return myMetaModifier.ComponentModifierLiasons
    End Function

    Public ReadOnly Property Liasons As PlaylistModifierUILiason()
        Get
            Return myLiasons
        End Get
    End Property

    Private Sub LoadModifierLiasons(ByVal modifiersDirectory As String)
        If (Directory.Exists(modifiersDirectory)) Then
            Dim modifierPaths As String() = Directory.GetFiles(modifiersDirectory, "*.xml", SearchOption.AllDirectories)

            Dim liasons As ArrayList = New ArrayList

            For Each path As String In modifierPaths
                liasons.Add(New PlaylistModifierUILiason(path, myModifiersDirectory))
            Next

            myLiasons = liasons.ToArray(GetType(PlaylistModifierUILiason))
        End If
    End Sub


    Private Shared Function GenerateMediaHashKey(ByRef media As Media)
        Return media.getItemInfo("Author") + media.getItemInfo("Title") + media.getItemInfo("AlbumID")
    End Function

End Class
