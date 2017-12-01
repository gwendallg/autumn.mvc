
API_KEY=$1
SOURCE=$2
VERSION=$3

echo 'nuget packaging'
dotnet pack --configuration Release --output ../../nupkgs /p:PackageVersion=$VERSION ./src/Autumn.Mvc/Autumn.Mvc.csproj

echo 'nuget push'
dotnet nuget push ./nupkgs/Autumn.Mvc.$VERSION.nupkg -k $API_KEY -s $SOURCE

