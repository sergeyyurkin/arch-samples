Param(
    [parameter(Mandatory=$false)][string]$appName="myapp",
    [parameter(Mandatory=$false)][bool]$deployInfrastructure=$true,
    [parameter(Mandatory=$false)][bool]$deployCharts=$true,
    [parameter(Mandatory=$false)][bool]$clean=$true
    )

if ($clean) {
    $listOfReleases=$(helm ls --filter $appName -q)
    if ([string]::IsNullOrEmpty($listOfReleases)) {
        Write-Host "No previous releases found!" -ForegroundColor Green
	}else{
        Write-Host "Previous releases found" -ForegroundColor Green
        Write-Host "Cleaning previous helm releases..." -ForegroundColor Green
        helm uninstall $listOfReleases
        Write-Host "Previous releases deleted" -ForegroundColor Green
	}        
}

Write-Host "Begin installation using Helm" -ForegroundColor Green

$infras = ("sql-data")
# $charts = ("ordering-api")

if ($deployInfrastructure) {
    foreach ($infra in $infras) {
        Write-Host "Installing infrastructure: $infra" -ForegroundColor Green
        helm install "$appName-$infra" --values app.yaml --values inf.yaml --set app.name=$appName $infra
    }
}
else {
    Write-Host "Infrastructure charts aren't installed (-deployCharts is false)" -ForegroundColor Yellow
}

if ($deployCharts) {
    foreach ($chart in $charts) {
        $options = "-f app.yaml --values inf.yaml --set app.name=$appName"
        $command = "install $appName-$chart $options $chart"
        Write-Host "Installing: $chart" -ForegroundColor Green
        Write-Host "Helm Command: helm $command" -ForegroundColor Gray
        Invoke-Expression 'cmd /c "helm $command"'
    }
}
else {
    Write-Host "Non-infrastructure charts aren't installed (-deployCharts is false)" -ForegroundColor Yellow
}

Write-Host "helm charts installed." -ForegroundColor Green
