Write-Host "Build docker image ..." -ForegroundColor Green
docker build -t "syyurkin/identity-api:v2" -f "src/Identity.Api/Dockerfile" "src/."

Write-Host "Helm dependency update ..." -ForegroundColor Green
helm dependency update "./src/Identity.Api/charts/"

Write-Host "Helm install identity-api ..." -ForegroundColor Green
helm install identity "./src/Identity.Api/charts/"

Write-Host "helm charts installed." -ForegroundColor Green