Param(
    [parameter(Mandatory=$false)][string]$appName="myapp"
)

$listOfReleases=$(helm ls --filter $appName -q)

if ([string]::IsNullOrEmpty($listOfReleases)) {
    Write-Host "No previous releases found!" -ForegroundColor Green
}else{
    Write-Host "Previous releases found" -ForegroundColor Green
    Write-Host "Cleaning previous helm releases..." -ForegroundColor Green
    helm uninstall $listOfReleases
    Write-Host "Previous releases deleted" -ForegroundColor Green
}