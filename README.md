<p align="center">
 <img src="https://github.com/gwendallg/autumn.mvc/blob/develop/logo128.png" width="25%" height="25%">
</p>

<p align="center">
Autumn.Mvc
</p>

Autumn.Mvc is aspNet Core extension that will make it easier for you to write your REST APIs. Its purpose is to convert a query in RSQL format to lamba expression.
	
## Supported Frameworks
- [x] .NET Core 2.0+

## Installation
```
dotnet add package Autumn.Mvc
```

## Quick Start
1. Add a reference to the NuGet Autumn.Mvc package in your project.
2. Modify Startup class in your project 
```csharp

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // autumn.mvc configuration
            services
                .AddAutumn(options =>
                    options
                        // ?query=  Query => query ( snake_case )
                        .QueryFieldName("Search")
                        // snake_case
                        .NamingStrategy(new SnakeCaseNamingStrategy())
                        .HostingEnvironment(HostingEnvironment))
		)
         }
```	

3. Add a new operation in your mvc controller 

```csharp
	// like Get operation
 	[HttpGet]
        public IActionResult Get(Expression<Func<[your model], bool>> filter,
            IPageable<[your model]> pageable)
	{
		// your code here
	}
```
4. Use for filter your data

Examples of RSQL expressions in both FIQL-like:

* ?query=name=="Kill Bill";year=gt=2003
* ?query=genres=in=(sci-fi,action);(director=='Christopher Nolan',actor==*Bale);year=ge=2000
* ?query=director.lastName==Nolan;year=ge=2000;year=lt=2010
* ?query=genres=in=(sci-fi,action);genres=out=(romance,animated,horror),director==Que*Tarantino

Examples of Pagination and sort expressions :

* ?pageNumber=0&pageSize=100&sort=name&name.dir=desc&sort=year&year.dir=acs

## And ...

* See Autumn.Mvc.Samples project for samples ...
* See https://github.com/jirutka/rsql-parser
* See http://tools.ietf.org/html/draft-nottingham-atompub-fiql-00
