Write-Host "Build docker images..." -ForegroundColor Green
docker build -t "syyurkin/identity-api:v2" -f "src/Identity.Api/Dockerfile" "src/."
docker build -t "syyurkin/persons-api:v2" -f "src/Persons.Api/Dockerfile" "src/."

Write-Host "Helm dependency update..." -ForegroundColor Green
helm dependency update "./src/Identity.Api/charts/"
helm dependency update "./src/Persons.Api/charts/"

Write-Host "Helm install identity-api..." -ForegroundColor Green
helm install identity "./src/Identity.Api/charts/"
helm install persons "./src/Persons.Api/charts/"

kubectl apply -f .\auth-ingress.yaml
kubectl apply -f .\app-ingress.yaml

Write-Host "helm charts installed." -ForegroundColor Green