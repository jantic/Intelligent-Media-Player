﻿Imports WMPLib
Imports AxWMPLib
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

    Public Sub GeneratePlaylist(ByRef player As AxWindowsMediaPlayer)
        InitializePlaylistForModification(player)
        myMetaModifier.ModifyPlaylist(player.currentPlaylist, player.mediaCollection, True)

        'Only go through the trouble if current playlist is a reasonable size (<10000).  Otherwise, this takes way too long.
        If (player.currentPlaylist.count < 10000) Then
            RemoveDuplicates(player)
        End If
    End Sub

    Private Sub InitializePlaylistForModification(ByRef player As AxWindowsMediaPlayer)
        If (myMetaModifier.NumberOfComponentModifiers > 0) Then
            If (myMetaModifier.Liason.ModifierAction.Name = "Add") Then
                player.currentPlaylist.clear()
            ElseIf ((myMetaModifier.Liason.ModifierAction.Name = "Subtract" Or (myMetaModifier.Liason.ModifierAction.Name = "Filter") And _
                     player.currentPlaylist.count = 0)) Then
                player.currentPlaylist = player.mediaCollection.getByAttribute("MediaType", "audio")
            End If
        Else
            player.currentPlaylist = player.mediaCollection.getByAttribute("MediaType", "audio")
        End If
    End Sub

    Private Sub RemoveDuplicates(ByRef player As AxWindowsMediaPlayer)

        Dim nonDuplicatedList As Dictionary(Of String, IWMPMedia) = New Dictionary(Of String, IWMPMedia)

        For x As Integer = 0 To player.currentPlaylist.count - 1 Step 1
            Dim key As String = PlaylistManager.GenerateMediaHashKey(player.currentPlaylist.Item(x))

            If (Not (nonDuplicatedList.ContainsKey(key))) Then
                nonDuplicatedList.Add(key, player.currentPlaylist.Item(x))
            End If
        Next

        player.currentPlaylist.clear()

        For Each mediaItem As IWMPMedia In nonDuplicatedList.Values.ToArray()
            player.currentPlaylist.appendItem(mediaItem)
        Next

    End Sub

    Public Sub AddWorkingModifier(ByVal liason As PlaylistModifierUILiason)
        If (File.Exists(liason.FilePath)) Then
            myMetaModifier.AddComponentModifier(liason)
        End If
    End Sub

    Public Sub RemoveWorkingModifier(ByVal index As UInteger)
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


    Private Shared Function GenerateMediaHashKey(ByRef media As IWMPMedia)
        Return media.getItemInfo("Artist") + media.getItemInfo("Title") + media.getItemInfo("Album")
    End Function

End Class
