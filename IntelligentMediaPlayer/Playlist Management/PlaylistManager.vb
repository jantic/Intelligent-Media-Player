Imports WMPLib
Imports AxWMPLib
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath


Partial Public Class PlaylistManager
    Public Enum ModifierType
        WMPAttribute
        LastFM
    End Enum

    Private myModifiersDirectory As String
    Private myLiasons As PlaylistModifierUILiason()
    Private myWorkingModifiers As ArrayList = New ArrayList
    Private myPreviouslyAppliedLastModifierIndex As Integer = -1

    Public Sub New(ByVal modifiersDirectory As String)
        myModifiersDirectory = modifiersDirectory
        LoadModifierLiasons(myModifiersDirectory)
    End Sub

    Public Sub GeneratePlaylist(ByRef player As AxWindowsMediaPlayer)

        If (myWorkingModifiers.Count > 0) Then

            Dim start As Integer = 0

            If (myPreviouslyAppliedLastModifierIndex <= -1) Then
                player.currentPlaylist.clear()
                myPreviouslyAppliedLastModifierIndex = -1
            Else
                start = myPreviouslyAppliedLastModifierIndex
            End If


            For index As Integer = start To myWorkingModifiers.Count - 1 Step 1
                Dim modifier As IPlaylistModifier = DirectCast(myWorkingModifiers.Item(index), IPlaylistModifier)

                If (index = 0 And (modifier.ModificationAction.Name = "Subtract" Or modifier.ModificationAction.Name = "Filter")) Then 'otherwise you're subtracting from nothing
                    player.currentPlaylist = player.mediaCollection.getByAttribute("MediaType", "audio")
                End If

                If (index = start And myPreviouslyAppliedLastModifierIndex >= 0) Then
                    modifier.ModifyPlaylist(player, True)
                Else
                    modifier.ModifyPlaylist(player, False)
                End If
            Next


        Else
            player.currentPlaylist = player.mediaCollection.getByAttribute("MediaType", "audio")
        End If

        myPreviouslyAppliedLastModifierIndex = myWorkingModifiers.Count - 1

        'Only go through the trouble if current playlist is a reasonable size (<10000).  Otherwise, this takes way too long.
        If (player.currentPlaylist.count < 10000) Then
            RemoveDuplicates(player)
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

            If (liason.Type = ModifierType.WMPAttribute) Then
                myWorkingModifiers.Add(New WMPAttributePlaylistModifier(liason))
            ElseIf (liason.Type = ModifierType.LastFM) Then
                myWorkingModifiers.Add(New LastFMPlaylistModifier(liason))
            End If

        End If
    End Sub

    Public Sub RemoveWorkingModifier(ByVal index As UInteger)
        myWorkingModifiers.RemoveAt(index)

        If (index <= myPreviouslyAppliedLastModifierIndex) Then
            myPreviouslyAppliedLastModifierIndex = index - 1
        End If
    End Sub

    Public Function GetWorkingModifiers() As PlaylistModifierUILiason()
        Dim modifiers As IPlaylistModifier() = myWorkingModifiers.ToArray(GetType(IPlaylistModifier))
        Dim liasons As ArrayList = New ArrayList()

        For Each modifier As IPlaylistModifier In modifiers
            liasons.Add(modifier.Liason)
        Next

        Return liasons.ToArray(GetType(PlaylistModifierUILiason))
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
                liasons.Add(New PlaylistModifierUILiason(path))
            Next

            myLiasons = liasons.ToArray(GetType(PlaylistModifierUILiason))
        End If
    End Sub


    Private Shared Function GenerateMediaHashKey(ByRef media As IWMPMedia)
        Return media.getItemInfo("Artist") + media.getItemInfo("Title") + media.getItemInfo("Album")
    End Function

End Class
