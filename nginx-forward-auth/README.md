# README.md


```
docker build -t syyurkin/identity-api:0.1.0 -f src/Identity.Api/Dockerfile src/.

```

```
helm dependency update ./src/Identity.Api/charts/

```

```
helm install identity ./src/Identity.Api/charts/
```