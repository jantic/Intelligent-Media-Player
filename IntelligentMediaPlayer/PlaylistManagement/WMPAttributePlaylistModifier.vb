Imports WMPLib
Imports AxWMPLib
Imports System.Xml
Imports System.Xml.XPath

Partial Public Class PlaylistManager

    Private Class WMPAttributePlaylistModifier
        Implements IPlaylistModifier

        Private myLiason As PlaylistModifierUILiason
        Private myAttributeMappings As PlaylistModifierAttributeMapping()
        Private myCachedPlaylist As ArrayList = New ArrayList


        Public Sub New(ByVal theLiason As PlaylistModifierUILiason)
            myLiason = New PlaylistModifierUILiason(theLiason)
            myAttributeMappings = PlaylistModifierAttributeMapping.LoadAttributeMappings(myLiason)
        End Sub

        Public ReadOnly Property ModificationAction As Action Implements IPlaylistModifier.ModificationAction
            Get
                Return myLiason.ModifierAction
            End Get
        End Property

        Public Sub ModifyPlaylist(ByRef player As AxWindowsMediaPlayer, Optional ByVal UseCachedResult As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist

            If (UseCachedResult And Not (myCachedPlaylist.Count > 0)) Then
                ApplyCachedPlaylist(player)
            Else

                Dim mc As WMPLib.IWMPMediaCollection2 = player.mediaCollection

                Dim query As WMPLib.IWMPQuery = mc.createQuery()

                For Each attributeMapping As PlaylistModifierAttributeMapping In myAttributeMappings

                    Dim attributeName As String = attributeMapping.Attribute
                    Dim attributeValue As String = attributeMapping.GetMatchingInputValue(myLiason.Inputs).Trim.ToLower

                    query.addCondition(attributeName, "Equals", attributeValue)
                Next

                Dim result As IWMPPlaylist = mc.getPlaylistByQuery(query, "audio", "", False)

                Dim quickMediaLookup As Dictionary(Of String, IWMPMedia) = New Dictionary(Of String, IWMPMedia) 'for proper, fast subtraction
                'this makes subtraction a O(N) operation as opposed to an O(N2) operation

                If (myLiason.ModifierAction = Action.Subtract) Then

                    For y As Integer = 0 To player.currentPlaylist.count - 1 Step 1

                        If (y >= player.currentPlaylist.count) Then 'need to check since we're potentially removing items.
                            Exit For
                        End If

                        Dim item As IWMPMedia = player.currentPlaylist.Item(y)
                        Dim key As String = GenerateMediaHashKey(item)

                        If (Not (quickMediaLookup.ContainsKey(key))) Then
                            quickMediaLookup.Add(key, item)
                        Else
                            player.currentPlaylist.removeItem(item)
                            y -= 1
                        End If
                    Next
                End If

                For index = 0 To result.count - 1 Step 1
                    Dim mediaItem As IWMPMedia = result.Item(index)

                    If (myLiason.ModifierAction = Action.Subtract) Then
                        Dim key As String = GenerateMediaHashKey(mediaItem)
                        If (quickMediaLookup.ContainsKey(key)) Then
                            player.currentPlaylist.removeItem(quickMediaLookup(GenerateMediaHashKey(mediaItem)))
                            quickMediaLookup.Remove(key)
                        End If
                    ElseIf (myLiason.ModifierAction = Action.Add) Then
                        player.currentPlaylist.appendItem(result.Item(index))
                    End If
                Next

                CacheThePlaylist(player)
                End If
        End Sub

        Public ReadOnly Property Liason As PlaylistModifierUILiason Implements IPlaylistModifier.Liason
            Get
                Return myLiason
            End Get
        End Property


        Private Sub CacheThePlaylist(ByRef player As AxWindowsMediaPlayer)
            myCachedPlaylist.Clear()

            For index As Integer = 0 To player.currentPlaylist.count - 1 Step 1
                myCachedPlaylist.Add(player.currentPlaylist.Item(index))
            Next
        End Sub

        Private Sub ApplyCachedPlaylist(ByRef player As AxWindowsMediaPlayer)

            If (myCachedPlaylist.Count > 0) Then
                player.currentPlaylist.clear()
                For Each mediaItem As IWMPMedia In myCachedPlaylist
                    player.currentPlaylist.appendItem(mediaItem)
                Next
            End If
        End Sub
       

        Private Class PlaylistModifierAttributeMapping
            Private myID As UInteger
            Private myAttribute As String
            Private myMapToID As UInteger

            Private Sub New(ByVal theId As UInteger, ByVal theAttribute As String, ByVal theMapToID As UInteger)
                myID = theId
                myAttribute = theAttribute
                myMapToID = theMapToID
            End Sub

            Public Shared Function LoadAttributeMappings(ByRef liason As PlaylistModifierUILiason) As PlaylistModifierAttributeMapping()
                Dim modifierFile As XmlDocument = New XmlDocument()
                modifierFile.Load(liason.FilePath)

                Dim mappings As ArrayList = New ArrayList
                Dim index As UInteger = 0
                Dim attribute As String = PlaylistManager.XMLGetValueAt(modifierFile, "//Method/MediaAttributeMatch/AttributeMappings/Mapping/WMP_Attribute", index)

                While (Not (attribute Is Nothing))
                    Dim ID As UInteger = UInteger.Parse(PlaylistManager.XMLGetValueAt(modifierFile, "//Method/MediaAttributeMatch/AttributeMappings/Mapping/ID", index))
                    Dim InputID As UInteger = UInteger.Parse(PlaylistManager.XMLGetValueAt(modifierFile, "//Method/MediaAttributeMatch/AttributeMappings/Mapping/InputID", index))

                    mappings.Add(New PlaylistModifierAttributeMapping(ID, attribute, InputID))

                    index += 1
                    attribute = PlaylistManager.XMLGetValueAt(modifierFile, "//Method/MediaAttributeMatch/AttributeMappings/Mapping/WMP_Attribute", index)
                End While

                Return mappings.ToArray(GetType(PlaylistModifierAttributeMapping))
            End Function

            Public ReadOnly Property ID As UInteger
                Get
                    Return myID
                End Get
            End Property

            Public ReadOnly Property Attribute As String
                Get
                    Return myAttribute
                End Get
            End Property

            Public ReadOnly Property MapToId As UInteger
                Get
                    Return myMapToID
                End Get
            End Property

            Public Function GetMatchingInputValue(ByRef inputValues As PlaylistModifierInput()) As String
                For Each input As PlaylistModifierInput In inputValues
                    If (input.ID = myMapToID) Then
                        Return input.Value
                    End If
                Next

                Return ""
            End Function


        End Class


    End Class




End Class
