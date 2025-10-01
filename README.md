## Push repo to git repo (This step is specifically for pushing the workspace to the for the first time)
cd <project workspace><br>
git init<br>
git add .<br>
git commit -m "your message"<br>
git branch -M main<br>
git remote add origin https://github.com/Jackson5240/UENCheck.git<br>
git push -u origin main<br>

## Pre-requisite
Installed .NET 5.0.408

## To run app
cd <project workspace><br>
dotend build<br>
dotend run<br>

## Access the app
Open the browser, access "https://localhost:5001"
