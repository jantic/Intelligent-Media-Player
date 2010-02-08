Imports WMPLib
Imports AxWMPLib
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

        Public Sub ModifyPlaylist(ByRef currentPlaylist As IWMPPlaylist, ByRef mediaCollection As IWMPMediaCollection2, Optional ByVal UseCachedResults As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist

            If (UseCachedResults And Not (myCachedPlaylist.Count > 0)) Then
                ApplyCachedPlaylist(currentPlaylist)
            Else

                Dim attributeLookup As Dictionary(Of String, String) = New Dictionary(Of String, String)

                For Each attributeMapping As PlaylistModifierAttributeMapping In myAttributeMappings

                    Dim attributeName As String = attributeMapping.Attribute
                    Dim attributeValue As String = attributeMapping.GetMatchingInputValue(myLiason.Inputs).Trim.ToLower
                    attributeLookup.Add(attributeName, attributeValue)
                Next

                Liason.ModifierAction.ModifyPlaylist(currentPlaylist, mediaCollection, attributeLookup)

                CacheThePlaylist(currentPlaylist)
            End If
        End Sub

        Public ReadOnly Property Liason As PlaylistModifierUILiason Implements IPlaylistModifier.Liason
            Get
                Return myLiason
            End Get
        End Property


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


    End Class




End Class
