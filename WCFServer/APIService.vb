' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
<ServiceContract()>
Public Interface IAPIService

    <OperationContract()>
    Function GetPolicy(ByVal oRequest As ModelContract.Policy.GetPolicy.GetPolicyRequestData) As ModelContract.Policy.GetPolicy.GetPolicyResponseData

    <OperationContract()>
    Function AddRequest(ByVal oRequest As ModelContract.Contact.AddRequest.AddRequestRequestData) As ModelContract.Contact.AddRequest.AddRequestResponseData

    <OperationContract()>
    Function UpdateMultipleRequestStatusFromSource(ByVal oRequest As ModelContract.Contact.UpdateMultipleRequestStatusFromSource.UpdateMultipleRequestStatusFromSourceRequestData) As ModelContract.Contact.UpdateMultipleRequestStatusFromSource.UpdateMultipleRequestStatusFromSourceResponseData

    <OperationContract()>
    Function UpdateRequestStatus(ByVal oRequest As ModelContract.Contact.UpdateRequestStatus.UpdateRequestStatusRequestData) As ModelContract.Contact.UpdateRequestStatus.UpdateRequestStatusResponseData

    <OperationContract()>
    Function RetrivePanelLink(ByVal oRequest As ModelContract.Contact.RetrivePanelLink.RetrivePanelLinkRequestData) As ModelContract.Contact.RetrivePanelLink.RetrivePanelLinkResponseData

    <OperationContract()>
    Function GetRichiesteAccettazioni(ByVal oRequest As ModelContract.Contact.GetRichiesteAccettazioni.GetRichiesteAccettazioniRequestData) As ModelContract.Contact.GetRichiesteAccettazioni.GetRichiesteAccettazioniResponseData

    <OperationContract()>
    Function GetConsensiContatto(ByVal oRequest As ModelContract.Contact.GetConsensiContatto.GetConsensiContattoRequestData) As ModelContract.Contact.GetConsensiContatto.GetConsensiContattoResponseData

    <OperationContract()>
    Function CheckPrivacyPolicyChanged(ByVal oRequest As ModelContract.Policy.CheckPolicy.CheckPolicyRequestData) As ModelContract.Policy.CheckPolicy.CheckPolicyResponseData

End Interface