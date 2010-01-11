<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainInterface
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainInterface))
        Me.AxWindowsMediaPlayer1 = New AxWMPLib.AxWindowsMediaPlayer()
        Me.PlaylistBox = New System.Windows.Forms.ListBox()
        Me.FilteredPlaylistLabel = New System.Windows.Forms.Label()
        Me.ArtistSimilarityGroupBox = New System.Windows.Forms.GroupBox()
        Me.ArtistSimilarityButton = New System.Windows.Forms.Button()
        Me.ArtistNameLabel = New System.Windows.Forms.Label()
        Me.ArtistNameTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TrackSimilarityGroupBox = New System.Windows.Forms.GroupBox()
        Me.TrackSimilarityButton = New System.Windows.Forms.Button()
        Me.TrackNameTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.TrackNameLabel = New System.Windows.Forms.Label()
        Me.ShuffleCheckBox = New System.Windows.Forms.CheckBox()
        Me.TagArtistLabel = New System.Windows.Forms.Label()
        Me.TagAlbumLabel = New System.Windows.Forms.Label()
        Me.TagTrackLabel = New System.Windows.Forms.Label()
        Me.ArtistTextLabel = New System.Windows.Forms.Label()
        Me.AlbumTextLabel = New System.Windows.Forms.Label()
        Me.TrackTextLabel = New System.Windows.Forms.Label()
        Me.ArtistSimilarityGroupBox.SuspendLayout()
        Me.TrackSimilarityGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxWindowsMediaPlayer1
        '
        Me.AxWindowsMediaPlayer1.Enabled = True
        Me.AxWindowsMediaPlayer1.Location = New System.Drawing.Point(579, 25)
        Me.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1"
        Me.AxWindowsMediaPlayer1.OcxState = CType(resources.GetObject("AxWindowsMediaPlayer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWindowsMediaPlayer1.Size = New System.Drawing.Size(278, 212)
        Me.AxWindowsMediaPlayer1.TabIndex = 0
        '
        'PlaylistBox
        '
        Me.PlaylistBox.FormattingEnabled = True
        Me.PlaylistBox.Location = New System.Drawing.Point(12, 25)
        Me.PlaylistBox.Name = "PlaylistBox"
        Me.PlaylistBox.Size = New System.Drawing.Size(553, 212)
        Me.PlaylistBox.TabIndex = 1
        '
        'FilteredPlaylistLabel
        '
        Me.FilteredPlaylistLabel.AutoSize = True
        Me.FilteredPlaylistLabel.Location = New System.Drawing.Point(9, 9)
        Me.FilteredPlaylistLabel.Name = "FilteredPlaylistLabel"
        Me.FilteredPlaylistLabel.Size = New System.Drawing.Size(76, 13)
        Me.FilteredPlaylistLabel.TabIndex = 4
        Me.FilteredPlaylistLabel.Text = "Filtered Playlist"
        '
        'ArtistSimilarityGroupBox
        '
        Me.ArtistSimilarityGroupBox.Controls.Add(Me.ArtistSimilarityButton)
        Me.ArtistSimilarityGroupBox.Controls.Add(Me.ArtistNameLabel)
        Me.ArtistSimilarityGroupBox.Controls.Add(Me.ArtistNameTextBox)
        Me.ArtistSimilarityGroupBox.Location = New System.Drawing.Point(12, 256)
        Me.ArtistSimilarityGroupBox.Name = "ArtistSimilarityGroupBox"
        Me.ArtistSimilarityGroupBox.Size = New System.Drawing.Size(269, 104)
        Me.ArtistSimilarityGroupBox.TabIndex = 5
        Me.ArtistSimilarityGroupBox.TabStop = False
        Me.ArtistSimilarityGroupBox.Text = "Filter Playlist by Artist Similarity"
        '
        'ArtistSimilarityButton
        '
        Me.ArtistSimilarityButton.Location = New System.Drawing.Point(9, 71)
        Me.ArtistSimilarityButton.Name = "ArtistSimilarityButton"
        Me.ArtistSimilarityButton.Size = New System.Drawing.Size(254, 27)
        Me.ArtistSimilarityButton.TabIndex = 5
        Me.ArtistSimilarityButton.Text = "Regenerate Playlist by Artist Similarity"
        Me.ArtistSimilarityButton.UseVisualStyleBackColor = True
        '
        'ArtistNameLabel
        '
        Me.ArtistNameLabel.AutoSize = True
        Me.ArtistNameLabel.Location = New System.Drawing.Point(6, 35)
        Me.ArtistNameLabel.Name = "ArtistNameLabel"
        Me.ArtistNameLabel.Size = New System.Drawing.Size(61, 13)
        Me.ArtistNameLabel.TabIndex = 4
        Me.ArtistNameLabel.Text = "Artist Name"
        '
        'ArtistNameTextBox
        '
        Me.ArtistNameTextBox.Location = New System.Drawing.Point(86, 32)
        Me.ArtistNameTextBox.Name = "ArtistNameTextBox"
        Me.ArtistNameTextBox.Size = New System.Drawing.Size(177, 20)
        Me.ArtistNameTextBox.TabIndex = 3
        '
        'TrackSimilarityGroupBox
        '
        Me.TrackSimilarityGroupBox.Controls.Add(Me.TrackSimilarityButton)
        Me.TrackSimilarityGroupBox.Controls.Add(Me.TrackNameTextBox)
        Me.TrackSimilarityGroupBox.Controls.Add(Me.TrackNameLabel)
        Me.TrackSimilarityGroupBox.Location = New System.Drawing.Point(287, 256)
        Me.TrackSimilarityGroupBox.Name = "TrackSimilarityGroupBox"
        Me.TrackSimilarityGroupBox.Size = New System.Drawing.Size(278, 104)
        Me.TrackSimilarityGroupBox.TabIndex = 6
        Me.TrackSimilarityGroupBox.TabStop = False
        Me.TrackSimilarityGroupBox.Text = "Filter Playlist by Track Similarity"
        '
        'TrackSimilarityButton
        '
        Me.TrackSimilarityButton.Location = New System.Drawing.Point(9, 71)
        Me.TrackSimilarityButton.Name = "TrackSimilarityButton"
        Me.TrackSimilarityButton.Size = New System.Drawing.Size(263, 27)
        Me.TrackSimilarityButton.TabIndex = 2
        Me.TrackSimilarityButton.Text = "Regenerate Playlist by Track Similarity"
        Me.TrackSimilarityButton.UseVisualStyleBackColor = True
        '
        'TrackNameTextBox
        '
        Me.TrackNameTextBox.Location = New System.Drawing.Point(79, 32)
        Me.TrackNameTextBox.Name = "TrackNameTextBox"
        Me.TrackNameTextBox.Size = New System.Drawing.Size(193, 20)
        Me.TrackNameTextBox.TabIndex = 1
        '
        'TrackNameLabel
        '
        Me.TrackNameLabel.AutoSize = True
        Me.TrackNameLabel.Location = New System.Drawing.Point(6, 35)
        Me.TrackNameLabel.Name = "TrackNameLabel"
        Me.TrackNameLabel.Size = New System.Drawing.Size(66, 13)
        Me.TrackNameLabel.TabIndex = 0
        Me.TrackNameLabel.Text = "Track Name"
        '
        'ShuffleCheckBox
        '
        Me.ShuffleCheckBox.AutoSize = True
        Me.ShuffleCheckBox.Location = New System.Drawing.Point(579, 337)
        Me.ShuffleCheckBox.Name = "ShuffleCheckBox"
        Me.ShuffleCheckBox.Size = New System.Drawing.Size(59, 17)
        Me.ShuffleCheckBox.TabIndex = 7
        Me.ShuffleCheckBox.Text = "Shuffle"
        Me.ShuffleCheckBox.UseVisualStyleBackColor = True
        '
        'TagArtistLabel
        '
        Me.TagArtistLabel.AutoSize = True
        Me.TagArtistLabel.Location = New System.Drawing.Point(576, 256)
        Me.TagArtistLabel.Name = "TagArtistLabel"
        Me.TagArtistLabel.Size = New System.Drawing.Size(36, 13)
        Me.TagArtistLabel.TabIndex = 8
        Me.TagArtistLabel.Text = "Artist: "
        '
        'TagAlbumLabel
        '
        Me.TagAlbumLabel.AutoSize = True
        Me.TagAlbumLabel.Location = New System.Drawing.Point(576, 276)
        Me.TagAlbumLabel.Name = "TagAlbumLabel"
        Me.TagAlbumLabel.Size = New System.Drawing.Size(39, 13)
        Me.TagAlbumLabel.TabIndex = 9
        Me.TagAlbumLabel.Text = "Album:"
        '
        'TagTrackLabel
        '
        Me.TagTrackLabel.AutoSize = True
        Me.TagTrackLabel.Location = New System.Drawing.Point(576, 295)
        Me.TagTrackLabel.Name = "TagTrackLabel"
        Me.TagTrackLabel.Size = New System.Drawing.Size(38, 13)
        Me.TagTrackLabel.TabIndex = 10
        Me.TagTrackLabel.Text = "Track:"
        '
        'ArtistTextLabel
        '
        Me.ArtistTextLabel.AutoSize = True
        Me.ArtistTextLabel.Location = New System.Drawing.Point(618, 256)
        Me.ArtistTextLabel.Name = "ArtistTextLabel"
        Me.ArtistTextLabel.Size = New System.Drawing.Size(0, 13)
        Me.ArtistTextLabel.TabIndex = 11
        '
        'AlbumTextLabel
        '
        Me.AlbumTextLabel.AutoSize = True
        Me.AlbumTextLabel.Location = New System.Drawing.Point(618, 276)
        Me.AlbumTextLabel.Name = "AlbumTextLabel"
        Me.AlbumTextLabel.Size = New System.Drawing.Size(0, 13)
        Me.AlbumTextLabel.TabIndex = 12
        '
        'TrackTextLabel
        '
        Me.TrackTextLabel.AutoSize = True
        Me.TrackTextLabel.Location = New System.Drawing.Point(618, 295)
        Me.TrackTextLabel.Name = "TrackTextLabel"
        Me.TrackTextLabel.Size = New System.Drawing.Size(0, 13)
        Me.TrackTextLabel.TabIndex = 13
        '
        'MainInterface
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 382)
        Me.Controls.Add(Me.TrackTextLabel)
        Me.Controls.Add(Me.AlbumTextLabel)
        Me.Controls.Add(Me.ArtistTextLabel)
        Me.Controls.Add(Me.TagTrackLabel)
        Me.Controls.Add(Me.TagAlbumLabel)
        Me.Controls.Add(Me.TagArtistLabel)
        Me.Controls.Add(Me.ShuffleCheckBox)
        Me.Controls.Add(Me.TrackSimilarityGroupBox)
        Me.Controls.Add(Me.ArtistSimilarityGroupBox)
        Me.Controls.Add(Me.FilteredPlaylistLabel)
        Me.Controls.Add(Me.PlaylistBox)
        Me.Controls.Add(Me.AxWindowsMediaPlayer1)
        Me.MaximumSize = New System.Drawing.Size(885, 420)
        Me.MinimumSize = New System.Drawing.Size(885, 420)
        Me.Name = "MainInterface"
        Me.Text = "Intelligent Media Player"
        Me.ArtistSimilarityGroupBox.ResumeLayout(False)
        Me.ArtistSimilarityGroupBox.PerformLayout()
        Me.TrackSimilarityGroupBox.ResumeLayout(False)
        Me.TrackSimilarityGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents PlaylistBox As System.Windows.Forms.ListBox
    Friend WithEvents FilteredPlaylistLabel As System.Windows.Forms.Label
    Friend WithEvents ArtistSimilarityGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ArtistNameLabel As System.Windows.Forms.Label
    Friend WithEvents ArtistNameTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents ArtistSimilarityButton As System.Windows.Forms.Button
    Friend WithEvents TrackSimilarityGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents TrackSimilarityButton As System.Windows.Forms.Button
    Friend WithEvents TrackNameTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents TrackNameLabel As System.Windows.Forms.Label
    Friend WithEvents ShuffleCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TagArtistLabel As System.Windows.Forms.Label
    Friend WithEvents TagAlbumLabel As System.Windows.Forms.Label
    Friend WithEvents TagTrackLabel As System.Windows.Forms.Label
    Friend WithEvents ArtistTextLabel As System.Windows.Forms.Label
    Friend WithEvents AlbumTextLabel As System.Windows.Forms.Label
    Friend WithEvents TrackTextLabel As System.Windows.Forms.Label

End Class
