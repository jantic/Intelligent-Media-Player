Imports WMPLib
Imports AxWMPLib
Imports System.Xml
Imports System.Xml.XPath
Imports System.IO

Partial Public Class PlaylistManager

    Private Class MetaPlaylistModifier
        Implements IPlaylistModifier

        Private myLiason As PlaylistModifierUILiason
        Private myComponentModifiers As New ArrayList
        Private myPreviouslyAppliedLastModifierIndex As Integer = -1

        Public Sub New(ByVal theLiason As PlaylistModifierUILiason)
            myLiason = New PlaylistModifierUILiason(theLiason)
            LoadComponentModifiers()
        End Sub


        Public Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, Optional ByVal UseCachedResults As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist
            Dim mediaPlayerCore As New WMPLib.WindowsMediaPlayerClass

            While (mediaPlayerCore.playlistCollection.getByName("temp").count > 0)
                mediaPlayerCore.remove(mediaPlayerCore.playlistCollection.getByName("temp").Item(0))
            End While

            Dim tempPlaylist As IWMPPlaylist = mediaPlayerCore.newPlaylist("temp")

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                tempPlaylist.appendItem(currentPlaylist.Item(index))
            Next

            Dim start As Integer = 0

            If (myPreviouslyAppliedLastModifierIndex <= -1 Or UseCachedResults = False) Then
                myPreviouslyAppliedLastModifierIndex = -1
            Else
                start = myPreviouslyAppliedLastModifierIndex
            End If


            For index As Integer = start To myComponentModifiers.Count - 1 Step 1
                Dim modifier As IPlaylistModifier = DirectCast(myComponentModifiers.Item(index), IPlaylistModifier)

                If (index = start And myPreviouslyAppliedLastModifierIndex >= 0) Then
                    modifier.ModifyPlaylist(tempPlaylist, mediaCollection, True)
                Else
                    modifier.ModifyPlaylist(tempPlaylist, mediaCollection, False)
                End If
            Next

            myLiason.ModifierAction.ModifyPlaylist(currentPlaylist, mediaCollection, tempPlaylist)

            mediaPlayerCore.playlistCollection.remove(tempPlaylist)
            myPreviouslyAppliedLastModifierIndex = myComponentModifiers.Count - 1

        End Sub

        Public Sub AddComponentModifier(ByVal liason As PlaylistModifierUILiason)
            myComponentModifiers.Add(LoadModifier(liason))
        End Sub

        Public Sub RemoveComponentModifier(ByVal index As UInteger)
            myComponentModifiers.RemoveAt(index)

            If (index <= myPreviouslyAppliedLastModifierIndex) Then
                myPreviouslyAppliedLastModifierIndex = index - 1
            End If
        End Sub

        Public ReadOnly Property ComponentModifierLiasons() As PlaylistModifierUILiason()
            Get
                Dim liasons As New ArrayList

                For Each modifier As IPlaylistModifier In myComponentModifiers
                    liasons.Add(modifier.Liason)
                Next

                Return liasons.ToArray(GetType(PlaylistModifierUILiason))
            End Get
        End Property

        Public ReadOnly Property NumberOfComponentModifiers()
            Get
                Return myComponentModifiers.Count
            End Get
        End Property

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
