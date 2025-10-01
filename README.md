## Push repo to git repo (This step is specifically for pushing the workspace to the for the first time)
```
cd <project workspace>
git init
git add .
git commit -m "your message"
git branch -M main
git remote add origin https://github.com/Jackson5240/UENCheck.git
git push -u origin main
```

## Pre-requisite
Installed .NET 5.0.408

## To run app
```
cd <project workspace>
dotend build
dotend run
```

## Access the app
Open the browser, access "https://localhost:5001"
