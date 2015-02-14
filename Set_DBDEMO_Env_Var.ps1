
$scriptpath = split-path -parent $myinvocation.mycommand.definition

Write-Host 'Setting "DBDEMO" environment variable to $scriptpath'
Set-ItemProperty -path HKCU:\Environment -name DBDEMO -value $scriptpath
Write-Host 'DONE'

$rebootresponse = Read-Host 'Do you want to reboot? (Y/N)'
If ($rebootresponse -eq 'Y')
{
    Restart-Computer
}
Else
{
    Write-Host 'If environment variable is not recognised you may need to restart later'
    Read-Host
}
