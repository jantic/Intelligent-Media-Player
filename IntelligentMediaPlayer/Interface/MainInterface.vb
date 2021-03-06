﻿Option Strict On
Option Explicit On

Imports System
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Text
Imports EnvDTE
Imports EnvDTE80



Public Class MainInterface
    Private player As MusicPlayer
    Private manager As PlaylistManager
    Private myMessageFilter As MessageFilter
    Private myDTE As EnvDTE80.DTE2
    Private myGUIAsyncUpdater As New GUIAsyncUpdater(Me)


    Private Sub Initialize() Handles Me.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        InitializePlayer()
        InitializePlaylist()
        InitializePlaylistModifierUI()
    End Sub

    Private Sub InitializeMessageHandler() 'got this ugly code from microsoft...it's a workaround- source:  http://msdn.microsoft.com/en-us/library/ms228772.aspx


        Dim obj As Object = Nothing
        Dim t As Type = Nothing
        'Get the ProgID for DTE 8.0.
        t = System.Type.GetTypeFromProgID("VisualStudio.DTE.8.0", True)
        'Create a new instance of the IDE.
        obj = System.Activator.CreateInstance(t, True)
        'Cast the instance to DTE2 and assign to variable dte.
        myDTE = DirectCast(obj, EnvDTE80.DTE2)
        'Register the IOleMessageFilter to handle any threading 
        'errors.
        MessageFilter.Register()
        'Display the Visual Studio IDE.
        myDTE.MainWindow.Activate()
    End Sub

    Private Sub Shutdown() Handles Me.Disposed

    End Sub

    Private Sub ShutdownMessageFilter()
        myDTE.Quit()
        'and turn off the IOleMessageFilter.
        MessageFilter.Revoke()
    End Sub

    Private Sub InitializePlayer()
        Try
            player = New MusicPlayer(AxWindowsMediaPlayer1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub InitializePlaylist()
        Try
            manager = New PlaylistManager("C:\Users\Jason\Documents\Visual Studio 2010\Projects\IntelligentMediaPlayer\IntelligentMediaPlayer\Playlist Modifier Plugins")
            PlaylistBox.Items.Clear()
            manager.GeneratePlaylist(player)
            FillPlaylistBox(player.currentPlaylist)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub InitializePlaylistModifierUI()
        Try
            AvailablePlaylistModifiersLB.Items.Clear()
            PopulateModifiersListsWithIcons()
            FillAvailableModifierListBox(manager.Liasons)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PopulateModifiersListsWithIcons()
        Try
            Dim imageListSmall As New ImageList()
            imageListSmall.ImageSize = New Drawing.Size(20, 20)
            imageListSmall.ColorDepth = ColorDepth.Depth32Bit
            Dim LastFMIcon As System.Drawing.Bitmap = GetIconImage("LastFMhq.bmp")
            imageListSmall.Images.Add(GetIconNameForModifierType(PlaylistManager.ModifierType.LastFM), LastFMIcon)
            Dim WMPIcon As System.Drawing.Bitmap = GetIconImage("WMPhq.bmp")
            imageListSmall.Images.Add(GetIconNameForModifierType(PlaylistManager.ModifierType.WMPAttribute), WMPIcon)
            Dim MetaIcon As System.Drawing.Bitmap = GetIconImage("MetaHQ.bmp")
            imageListSmall.Images.Add(GetIconNameForModifierType(PlaylistManager.ModifierType.Meta), MetaIcon)
            AvailablePlaylistModifiersLB.SmallImageList = imageListSmall
            ActivePlaylistModifiersLB.SmallImageList = imageListSmall
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillAvailableModifierListBox(ByRef liasons As PlaylistManager.PlaylistModifierUILiason())

        For Each liason As PlaylistManager.PlaylistModifierUILiason In liasons
            Dim imageName As String = GetIconNameForModifierType(liason.Type)
            AvailablePlaylistModifiersLB.Items.Add(liason.DisplayName, imageName)
        Next
    End Sub

    Private Function GetIconImage(ByVal imageName As String) As System.Drawing.Bitmap
        'Dim res() As String = GetType(MainInterface).Assembly.GetManifestResourceNames()
        Dim lookUpName As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "." + imageName
        Return New System.Drawing.Bitmap(GetType(MainInterface).Assembly.GetManifestResourceStream(lookUpName), True)
    End Function

    Private Function GetIconNameForModifierType(ByRef modType As PlaylistManager.ModifierType) As String
        Return modType.ToString()
    End Function



    Private Sub PlaylistBox_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlaylistBox.DoubleClick
        If (PlaylistBox.SelectedIndices.Count > 0) Then
            Dim selectedSong As Media = player.currentPlaylist.Item(PlaylistBox.SelectedIndices.Item(0))

            Try
                player.controls.playItem(selectedSong)
            Catch ex As Exception
                MsgBox(ex.Message())
            End Try
        End If
    End Sub

    Private Sub ToggleShuffle()
        player.ToggleShuffle(ShuffleCheckBox.Checked)
    End Sub

    Private Sub ShuffleCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShuffleCheckBox.CheckedChanged
        ToggleShuffle()
    End Sub

    Private Sub player_CurrentItemChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_CurrentItemChangeEvent) Handles AxWindowsMediaPlayer1.CurrentItemChange
        ' Display the name of the new media item.
        Dim currentArtist As String = player.currentMedia.getItemInfo("Author")
        ArtistTextLabel.Text = currentArtist
        Dim currentAlbum As String = player.currentMedia.getItemInfo("AlbumID")
        AlbumTextLabel.Text = currentAlbum
        TrackTextLabel.Text = player.currentMedia.getItemInfo("Title")
        AlbumYearText.Text = player.currentMedia.getItemInfo("ReleaseDateYear")
        NumberOfItemsText.Text = player.currentPlaylist.count.ToString

        Dim currentIndex As Integer = player.CurrentMediaIndex


        If (currentIndex >= 0) Then
            If (currentIndex < PlaylistBox.Items.Count) Then
                PlaylistBox.Focus()
                PlaylistBox.Items(currentIndex).Selected = True
                PlaylistBox.Items(currentIndex).EnsureVisible()
                PlaylistBox.Items.Item(currentIndex).Focused = True
            End If
        End If

        myGUIAsyncUpdater.DisplayInfoForAlbum(currentArtist, currentAlbum)
        myGUIAsyncUpdater.DisplayInfoForArtist(currentArtist)
    End Sub



    Private Sub player_CurrentPlaylistChange(ByVal sender As Object, ByVal e As AxWMPLib._WMPOCXEvents_CurrentPlaylistChangeEvent) Handles AxWindowsMediaPlayer1.CurrentPlaylistChange

        ' Display the name of the new media item.

        NumberOfItemsText.Text = player.currentPlaylist.count.ToString
    End Sub

    Private Sub Initialize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub AddModifierButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddModifierButton.Click

        If (AvailablePlaylistModifiersLB.SelectedItems().Item(0).Index >= 0) Then
            Dim liason As PlaylistManager.PlaylistModifierUILiason = manager.Liasons().ElementAt(AvailablePlaylistModifiersLB.SelectedItems().Item(0).Index)

            Dim myInputTextBoxes As ArrayList = New ArrayList
            myInputTextBoxes.Add(PlaylistModifierInput1)
            myInputTextBoxes.Add(PlaylistModifierInput2)
            myInputTextBoxes.Add(PlaylistModifierInput3)

            Dim index As Integer = 0

            For Each input As PlaylistManager.PlaylistModifierInput In liason.Inputs
                input.Value = DirectCast(myInputTextBoxes.Item(index), System.Windows.Forms.MaskedTextBox).Text
                index += 1
            Next

            liason.ModifierAction = GetSelectedAction()

            manager.AddWorkingModifier(liason)

            For Each inputTextBox As MaskedTextBox In myInputTextBoxes
                inputTextBox.Clear()
            Next

            UpdateActiveModifiersLB()
        End If
    End Sub

    Private Function GetSelectedAction() As PlaylistManager.IModifierAction
        If (FilterRB.Checked) Then
            Return New PlaylistManager.ModifierAction_Filter
        ElseIf (AddRB.Checked) Then
            Return New PlaylistManager.ModifierAction_Add
        Else
            Return New PlaylistManager.ModifierAction_Subtract
        End If
    End Function

    Private Sub SetSelectedAction(ByVal theAction As PlaylistManager.IModifierAction)
        If (theAction.Name = "Add") Then
            AddRB.Checked = True
        ElseIf (theAction.Name = "Filter") Then
            FilterRB.Checked = True
        Else
            RemoveRb.Checked = True
        End If
    End Sub

    Private Sub RemoveModifierButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveModifierButton.Click
        If (ActivePlaylistModifiersLB.SelectedItems().Count > 0) Then
            Dim selectedIndex As Integer = ActivePlaylistModifiersLB.SelectedItems().Item(0).Index
            If ((selectedIndex <= manager.GetWorkingModifiers.Length - 1) And selectedIndex > -1) Then
                manager.RemoveWorkingModifier(selectedIndex)
                UpdateActiveModifiersLB()
            End If
        End If
    End Sub


    Private Sub AvailablePlaylistModifiersLB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AvailablePlaylistModifiersLB.SelectedIndexChanged
        If (AvailablePlaylistModifiersLB.SelectedItems().Count > 0) Then
            UpdateModiferInputsUI(manager.Liasons().ElementAt(AvailablePlaylistModifiersLB.SelectedItems().Item(0).Index))
        End If
    End Sub

    Private Sub UpdateModiferInputsUI(ByRef liason As PlaylistManager.PlaylistModifierUILiason)
        Dim myInputLabels As ArrayList = New ArrayList
        myInputLabels.Add(Input1Label)
        myInputLabels.Add(Input2Label)
        myInputLabels.Add(Input3Label)

        For Each inputLabel As System.Windows.Forms.Label In myInputLabels
            inputLabel.ResetText()
            inputLabel.Visible = False
        Next

        Dim myInputTextBoxes As ArrayList = New ArrayList
        myInputTextBoxes.Add(PlaylistModifierInput1)
        myInputTextBoxes.Add(PlaylistModifierInput2)
        myInputTextBoxes.Add(PlaylistModifierInput3)


        For Each inputTextBox As System.Windows.Forms.MaskedTextBox In myInputTextBoxes
            inputTextBox.Clear()
            inputTextBox.Visible = False
        Next

        Dim index As Integer = 0

        For Each input As PlaylistManager.PlaylistModifierInput In liason.Inputs
            DirectCast(myInputLabels.Item(index), System.Windows.Forms.Label).Visible = True
            DirectCast(myInputLabels.Item(index), System.Windows.Forms.Label).Text = input.DisplayName
            DirectCast(myInputTextBoxes.Item(index), System.Windows.Forms.MaskedTextBox).Visible = True
            index += 1
        Next

        SetSelectedAction(liason.ModifierAction)

    End Sub

    Private Sub UpdateActiveModifiersLB()
        ActivePlaylistModifiersLB.Items.Clear()

        Dim workingModifiers As PlaylistManager.PlaylistModifierUILiason() = manager.GetWorkingModifiers()

        If (Not (workingModifiers Is Nothing)) Then
            For Each liason As PlaylistManager.PlaylistModifierUILiason In workingModifiers

                Dim displayText As String = "[" + liason.ModifierAction.Name + "] " + liason.DisplayName + "("

                For Each input As PlaylistManager.PlaylistModifierInput In liason.Inputs
                    displayText = displayText + input.DisplayName + " - " + input.Value + ";"
                Next

                displayText = displayText.TrimEnd(Convert.ToChar(";"))
                displayText = displayText + ")"
                Dim imageName As String = GetIconNameForModifierType(liason.Type)
                ActivePlaylistModifiersLB.Items.Add(displayText, imageName)
            Next
        End If

    End Sub


    Private Sub GeneratePlaylistButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GeneratePlaylistButton.Click
        player.controls.stop()
        'leaving shuffle on during this process slows things down big time.  
        player.ToggleShuffle(False)

        manager.GeneratePlaylist(player)

        ToggleShuffle()
        FillPlaylistBox(player.currentPlaylist)
    End Sub

    Private Sub FillPlaylistBox(ByVal currentPlaylist As Playlist)

        PlaylistBox.Items.Clear()

        Dim myListViewItems(currentPlaylist.count - 1) As System.Windows.Forms.ListViewItem

        Parallel.For(0, myListViewItems.Count,
        Sub(y As Integer)
            Try
                Dim media As Media = currentPlaylist.Item(y)
                myListViewItems(y) = New ListViewItem()
                myListViewItems(y).SubItems(0) = (New ListViewItem.ListViewSubItem(myListViewItems(y), media.getItemInfo("Author")))
                myListViewItems(y).SubItems.Add(New ListViewItem.ListViewSubItem(myListViewItems(y), media.getItemInfo("AlbumID")))
                myListViewItems(y).SubItems.Add(New ListViewItem.ListViewSubItem(myListViewItems(y), media.name))
                myListViewItems(y).SubItems.Add(New ListViewItem.ListViewSubItem(myListViewItems(y), media.getItemInfo("ReleaseDateYear")))
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Sub)

        PlaylistBox.Items.AddRange(myListViewItems)
    End Sub


    Private Function GetTorrentzURLForArtistSearch(ByVal artist As String) As String
        Dim baseURL As String = "http://www.torrentz.com/search?q="
        Dim searchString As String = artist.Replace(" ", "+")
        Return baseURL + searchString
    End Function

    Private Function GetTorrentzURLForAlbumSearch(ByVal artist As String, ByVal album As String) As String
        Dim baseURL As String = "http://www.torrentz.com/search?q="
        Dim searchString As String = artist.Replace(" ", "+") + "+" + album.Replace(" ", "+")
        Return baseURL + searchString
    End Function

    Private Sub SimilarArtistsLV_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimilarArtistsLV.DoubleClick

        If (Not SimilarArtistsLV.SelectedIndices Is Nothing) Then

            If (SimilarArtistsLV.SelectedIndices.Count > 0) Then

                Dim selectedArtist As String = SimilarArtistsLV.Items.Item(SimilarArtistsLV.SelectedIndices(0)).Text
                System.Diagnostics.Process.Start(GetTorrentzURLForArtistSearch(selectedArtist))
            End If
        End If
    End Sub

    Private Sub TopAlbumsLV_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TopAlbumsLV.DoubleClick
        If (Not TopAlbumsLV.SelectedIndices Is Nothing) Then

            If (TopAlbumsLV.SelectedIndices.Count > 0) Then
                Dim artist As String = ArtistTextLabel.Text
                Dim selectedAlbum As String = TopAlbumsLV.Items.Item(TopAlbumsLV.SelectedIndices(0)).Text
                System.Diagnostics.Process.Start(GetTorrentzURLForAlbumSearch(artist, selectedAlbum))
            End If
        End If
    End Sub

    Private Sub ArtistPictureBox_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArtistPictureBox.DoubleClick
        Dim artist As String = ArtistTextLabel.Text
        System.Diagnostics.Process.Start(GetTorrentzURLForArtistSearch(artist))
    End Sub


    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        Dim saveDialog As New System.Windows.Forms.SaveFileDialog()
        saveDialog.InitialDirectory = manager.ModifiersDirectory
        saveDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
        saveDialog.FilterIndex = 0
        saveDialog.RestoreDirectory = True

        If saveDialog.ShowDialog() = DialogResult.OK Then
            Dim path As String = saveDialog.FileName
            manager.SaveModiferSequenceAsMetaModifier(saveDialog.FileName)
        End If

        InitializePlaylistModifierUI() 'to refresh with newly saved modifier
    End Sub


End Class
