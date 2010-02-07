Imports WMPLib
Imports AxWMPLib
Imports System.Xml
Imports System.Xml.XPath
Imports System.IO

Partial Public Class PlaylistManager

    Private Class MetaPlaylistModifier
        Implements IPlaylistModifier

        Private myLiason As PlaylistModifierUILiason
        Private myCachedPlaylist As New ArrayList
        Private myComponentModifiers As New ArrayList

        Public Sub New(ByVal theLiason As PlaylistModifierUILiason)
            myLiason = New PlaylistModifierUILiason(theLiason)
            LoadComponentModifiers()
        End Sub


        Public Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, Optional ByVal UseCachedResult As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist

            If (UseCachedResult And Not (myCachedPlaylist.Count = 0)) Then
                ApplyCachedPlaylist(currentPlaylist)
            Else
                'run through component modifiers here.  Apply changes to temporary playlist, then use the appropriate action
                'on the current playlist.


                Dim mediaPlayerCore As New WMPLib.WindowsMediaPlayerClass


                If (mediaPlayerCore.playlistCollection.getByName("temp").count > 0) Then
                    mediaPlayerCore.remove(mediaPlayerCore.playlistCollection.getByName("temp").Item(0))
                End If

                Dim tempPlaylist As IWMPPlaylist = mediaPlayerCore.newPlaylist("temp")


                For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                    tempPlaylist.appendItem(currentPlaylist.Item(index))

                Next

                For Each modifier As IPlaylistModifier In myComponentModifiers
                    modifier.ModifyPlaylist(tempPlaylist, mediaCollection, False)
                Next

                myLiason.ModifierAction.ModifyPlaylist(currentPlaylist, mediaCollection, tempPlaylist)

                mediaPlayerCore.playlistCollection.remove(tempPlaylist)
                CacheThePlaylist(currentPlaylist)
            End If
        End Sub

        Private Sub LoadComponentModifiers()
            Dim metaModifierPath As String = myLiason.FilePath

            If (File.Exists(metaModifierPath)) Then
                Dim metaXML As XmlDocument = New XmlDocument
                metaXML.Load(metaModifierPath)

                Dim modifierNodes As XmlNodeList = metaXML.GetElementsByTagName("Modifier")


                For Each modifierElement As XmlElement In modifierNodes
                    Dim liason As New PlaylistModifierUILiason(modifierElement, PlaylistModifierUILiason.ModifiersDirectory)
                    Dim modifier As IPlaylistModifier = LoadModifier(liason)
                    If (Not modifier Is Nothing) Then
                        myComponentModifiers.Add(modifier)
                    End If
                Next
            End If
        End Sub

        Private Function LoadModifier(ByVal liason As PlaylistModifierUILiason) As IPlaylistModifier
            If (liason.Type = ModifierType.WMPAttribute) Then
                Return New WMPAttributePlaylistModifier(liason)
            ElseIf (liason.Type = ModifierType.LastFM) Then
                Return New LastFMPlaylistModifier(liason)
            ElseIf (liason.Type = ModifierType.Meta) Then
                Return New MetaPlaylistModifier(liason)
            End If

            Return Nothing
        End Function


        Private Sub CacheThePlaylist(ByRef currentPlaylist As IWMPPlaylist)
            myCachedPlaylist.Clear()

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                myCachedPlaylist.Add(currentPlaylist.Item(index))
            Next
        End Sub

        Private Sub ApplyCachedPlaylist(ByRef currentPlaylist As IWMPPlaylist)

            If (myCachedPlaylist.Count > 0) Then
                currentPlaylist.clear()
                For Each mediaItem As IWMPMedia In myCachedPlaylist
                    currentPlaylist.appendItem(mediaItem)
                Next
            End If
        End Sub


        Public ReadOnly Property Liason As PlaylistModifierUILiason Implements IPlaylistModifier.Liason
            Get
                Return myLiason
            End Get
        End Property

        Public ReadOnly Property ModificationAction As IModifierAction Implements IPlaylistModifier.ModificationAction
            Get
                Return myLiason.ModifierAction
            End Get
        End Property

    End Class

End Class
