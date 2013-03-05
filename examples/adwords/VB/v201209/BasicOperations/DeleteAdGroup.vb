' Copyright 2012, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201209

Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace Google.Api.Ads.AdWords.Examples.VB.v201209
  ''' <summary>
  ''' This code example deletes an ad group by setting the status to 'DELETED'.
  ''' To get ad groups, run GetAdGroups.vb.
  '''
  ''' Tags: AdGroupService.mutate
  ''' </summary>
  Public Class DeleteAdGroup
    Inherits ExampleBase
    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As New DeleteAdGroup
      Console.WriteLine(codeExample.Description)
      Try
        Dim adGroupId As Long = Long.Parse("INSERT_ADGROUP_ID_HERE")
        codeExample.Run(New AdWordsUser, adGroupId)
      Catch ex As Exception
        Console.WriteLine("An exception occurred while running this code example. {0}", _
            ExampleUtilities.FormatException(ex))
      End Try
    End Sub

    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example deletes an ad group by setting the status to 'DELETED'. " & _
            "To get ad groups, run GetAdGroups.vb."
      End Get
    End Property

    ''' <summary>
    ''' Runs the code example.
    ''' </summary>
    ''' <param name="user">The AdWords user.</param>
    ''' <param name="adGroupId">Id of the ad group to be deleted.</param>
    Public Sub Run(ByVal user As AdWordsUser, ByVal adGroupId As Long)
      ' Get the AdGroupService.
      Dim adGroupService As AdGroupService = user.GetService(AdWordsService.v201209.AdGroupService)

      ' Create ad group with DELETED status.
      Dim adGroup As New AdGroup
      adGroup.id = adGroupId

      ' When deleting an ad group, rename it to avoid name collisions with new
      ' ad groups.
      adGroup.name = "Deleted AdGroup - " + ExampleUtilities.GetRandomString
      adGroup.status = AdGroupStatus.DELETED

      ' Create the operation.
      Dim operation As New AdGroupOperation
      operation.operand = adGroup
      operation.operator = [Operator].SET

      Try
        ' Delete the ad group.
        Dim retVal As AdGroupReturnValue = adGroupService.mutate( _
            New AdGroupOperation() {operation})

        ' Display the results.
        If ((Not retVal Is Nothing) AndAlso (Not retVal.value Is Nothing) AndAlso _
            (retVal.value.Length > 0)) Then
          Dim deletedAdGroup As AdGroup = retVal.value(0)
          Console.WriteLine("Ad group with id = ""{0}"" was renamed to ""{1}"" and deleted.", _
              deletedAdGroup.id, deletedAdGroup.name)
        Else
          Console.WriteLine("No ad groups were deleted.")
        End If
      Catch ex As Exception
        Throw New System.ApplicationException("Failed to delete ad groups.", ex)
      End Try
    End Sub
  End Class
End Namespace
