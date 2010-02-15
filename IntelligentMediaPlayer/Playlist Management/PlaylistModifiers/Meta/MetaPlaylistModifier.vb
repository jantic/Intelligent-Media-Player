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

        Public Sub SaveAs(ByVal path As String)
            Dim saveXML As New XmlDocument
            saveXML.AppendChild(saveXML.CreateElement("PlaylistModifier"))
            If (myLiason.DisplayName = "") Then
                myLiason.DisplayName = System.IO.Path.GetFileNameWithoutExtension(path)
            End If
            InsertTypeElement(saveXML)
            InsertUIElement(saveXML)
            InsertActionElement(saveXML)
            InsertModifierKeyElement(saveXML)
            InsertMethodElement(saveXML)
            saveXML.Save(path)
        End Sub


        Public Sub ModifyPlaylist(ByRef currentPlaylist As Playlist, ByRef mediaCollection As MediaCollection, Optional ByVal UseCachedResults As Boolean = False) Implements IPlaylistModifier.ModifyPlaylist
            If (myComponentModifiers.Count > 0) Then

                Dim tempPlaylist As New Playlist

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
                myPreviouslyAppliedLastModifierIndex = myComponentModifiers.Count - 1
            End If
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

        Private Sub InsertTypeElement(ByRef saveXML As XmlDocument)
            Dim typeElement As XmlElement = saveXML.CreateElement("Type")
            typeElement.InnerText = "Meta"
            saveXML.DocumentElement.AppendChild(typeElement)
        End Sub

        Private Sub InsertUIElement(ByRef saveXML As XmlDocument)
            Dim uiElement As XmlElement = saveXML.CreateElement("UI")
            Dim displayNameElement As XmlElement = saveXML.CreateElement("DisplayName")
            displayNameElement.InnerText = myLiason.DisplayName
            uiElement.AppendChild(displayNameElement)
            Dim inputsElement As XmlElement = saveXML.CreateElement("Inputs")
            uiElement.AppendChild(inputsElement)
            saveXML.DocumentElement.AppendChild(uiElement)
        End Sub

        Private Sub InsertActionElement(ByRef saveXML As XmlDocument)
            Dim actionElement As XmlElement = saveXML.CreateElement("Action")
            actionElement.InnerText = myLiason.ModifierAction.Name
            saveXML.DocumentElement.AppendChild(actionElement)
        End Sub

        Private Sub InsertModifierKeyElement(ByRef saveXML As XmlDocument)
            Dim modifierKeyElement As XmlElement = saveXML.CreateElement("ModifierKey")
            modifierKeyElement.InnerText = Guid.NewGuid().ToString
            saveXML.DocumentElement.AppendChild(modifierKeyElement)
        End Sub

        Private Sub InsertMethodElement(ByRef saveXML As XmlDocument)
            Dim methodElement As XmlElement = saveXML.CreateElement("Method")
            Dim metaElement As XmlElement = saveXML.CreateElement("Meta")
            methodElement.AppendChild(metaElement)
            Dim componentsElement As XmlElement = saveXML.CreateElement("Components")
            metaElement.AppendChild(componentsElement)
            InsertModifierElementsIntoComponentsElement(saveXML, componentsElement)
            saveXML.DocumentElement.AppendChild(methodElement)
        End Sub

        Private Sub InsertModifierElementsIntoComponentsElement(ByRef saveXML As XmlDocument, ByRef componentsElement As XmlElement)
            Dim modifierID As Integer = 1
            For Each modifier As IPlaylistModifier In myComponentModifiers
                Dim modifierElement As XmlElement = CreateXMLElementForModifier(saveXML, modifier, modifierID)
                componentsElement.AppendChild(modifierElement)
                modifierID += 1
            Next
        End Sub

        Private Function CreateXMLElementForModifier(ByRef saveXML As XmlDocument, ByRef modifier As IPlaylistModifier, ByVal modifierID As Integer) As XmlElement
            Dim modifierElement As XmlElement = saveXML.CreateElement("Modifier")
            Dim idElement As XmlElement = saveXML.CreateElement("ID")
            idElement.InnerText = modifierID
            modifierElement.AppendChild(idElement)
            Dim modifierKeyElement As XmlElement = saveXML.CreateElement("ModifierKey")
            modifierKeyElement.InnerText = modifier.Liason.ModifierKey
            modifierElement.AppendChild(modifierKeyElement)
            Dim inputsElement As XmlElement = saveXML.CreateElement("Inputs")

            For Each input As PlaylistModifierInput In modifier.Liason.Inputs
                Dim inputElement As XmlElement = CreateXMLElementForInput(saveXML, input)
                inputsElement.AppendChild(inputElement)
            Next

            modifierElement.AppendChild(inputsElement)
            Dim actionElement As XmlElement = saveXML.CreateElement("Action")
            actionElement.InnerText = modifier.Liason.ModifierAction.Name
            modifierElement.AppendChild(actionElement)

            Return modifierElement
        End Function

        Private Function CreateXMLElementForInput(ByRef saveXML As XmlDocument, ByRef input As PlaylistModifierInput) As XmlElement
            Dim inputElement As XmlElement = saveXML.CreateElement("Input")
            Dim idElement As XmlElement = saveXML.CreateElement("ID")
            idElement.InnerText = input.ID
            Dim valueElement As XmlElement = saveXML.CreateElement("Value")
            valueElement.InnerText = input.Value
            inputElement.AppendChild(idElement)
            inputElement.AppendChild(valueElement)
            Return inputElement
        End Function

    End Class

    

End Class
