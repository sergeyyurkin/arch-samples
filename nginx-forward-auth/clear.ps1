Write-Host "helm uninstall" -ForegroundColor Green
helm uninstall identity

Write-Host "delete pvc" -ForegroundColor Green
kubectl delete pvc --all
