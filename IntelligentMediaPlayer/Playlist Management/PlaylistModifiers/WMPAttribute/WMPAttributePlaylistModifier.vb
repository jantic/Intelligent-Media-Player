Imports System.Xml
Imports System.Xml.XPath

Partial Public Class PlaylistManager

    Partial Private Class WMPAttributePlaylistModifier
        Implements IPlaylistModifier

        Private myLiason As PlaylistModifierUILiason
        Private myAttributeMappings As PlaylistModifierAttributeMapping()
        Private myCachedPlaylist As ArrayList = New ArrayList


        Public Sub New(ByVal theLiason As PlaylistModifierUILiason)
            myLiason = New PlaylistModifierUILiason(theLiason)
            myAttributeMappings = PlaylistModifierAttributeMapping.LoadAttributeMappings(myLiason)
        End Sub

        Public ReadOnly Property ModificationAction As IModifierAction Implements IPlaylistModifier.ModificationAction
            Get
                Return Liason.ModifierAction
            End Get
        End Property

        Public Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, Optional ByVal UseCachedResults As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist

            If (UseCachedResults And Not (myCachedPlaylist.Count > 0)) Then
                ApplyCachedPlaylist(currentPlaylist)
            Else
                Dim attributeLookupArray As New ArrayList()
                Dim attributeLookup As Dictionary(Of String, String) = New Dictionary(Of String, String)

                For Each attributeMapping As PlaylistModifierAttributeMapping In myAttributeMappings

                    Dim attributeName As String = attributeMapping.Attribute
                    Dim attributeValue As String = attributeMapping.GetMatchingInputValue(myLiason.Inputs).Trim.ToLower
                    attributeLookup.Add(attributeName, attributeValue)
                Next

                attributeLookupArray.Add(attributeLookup)
                Dim lookupArray() As Dictionary(Of String, String) = attributeLookupArray.ToArray(GetType(Dictionary(Of String, String)))

                Liason.ModifierAction.ModifyPlaylist(currentPlaylist, mediaCollection, lookupArray)

                CacheThePlaylist(currentPlaylist)
            End If
        End Sub

        Public ReadOnly Property Liason As PlaylistModifierUILiason Implements IPlaylistModifier.Liason
            Get
                Return myLiason
            End Get
        End Property


        Private Sub CacheThePlaylist(ByRef currentPlaylist As Playlist)
            myCachedPlaylist.Clear()

            For index As Integer = 0 To currentPlaylist.count - 1 Step 1
                myCachedPlaylist.Add(currentPlaylist.Item(index))
            Next
        End Sub

        Private Sub ApplyCachedPlaylist(ByRef currentPlaylist As Playlist)

            If (myCachedPlaylist.Count > 0) Then
                currentPlaylist.clear()
                For Each mediaItem As Media In myCachedPlaylist
                    currentPlaylist.appendItem(mediaItem)
                Next
            End If
        End Sub


    End Class




End Class
