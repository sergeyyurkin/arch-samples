Write-Host "helm uninstall" -ForegroundColor Green
helm uninstall identity
helm uninstall persons

Write-Host "delete pvc" -ForegroundColor Green
kubectl delete pvc --all

kubectl delete -f .\auth-ingress.yaml
kubectl delete -f .\app-ingress.yaml