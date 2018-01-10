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
                        // snake_case "Search" convert to "search"
                        .NamingStrategy(new SnakeCaseNamingStrategy())
                        .HostingEnvironment(HostingEnvironment))
            ...
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

* return all items where **name equal Kill Bill and year > 2003"**
    - **?query=name=="Kill Bill";year=gt=2003** *( or ?search=name=="Kill Bill";year>2003 )*
 
* return all items where **name start with "Kill" or address.suite is not null** 
    - ?query=name=Kill*,address.suite=is-null=false

* return all items where **debit<=credit**
    - ?query=debit<=credit *( or ?query=debit=le=credit )*

* return all items where **address.suite equals 'L' or equals 'E'**
    - ?query==address.suite=in=(L,E)

| Binary operator||
|-|-|
|and| **;**|
|or| **,**|

| Comparison operator||
|-|-|
|equals|**==** or **=eq=**|
|not equals|**!=** or **=neq=**|
|lower than|**<** or **=lt=**|
|lower than or equals|**<=** or **=le=**|
|greater than|**>** or **=gt=**|
|greater than or equals|**>=** or **=ge=**|
|in|**=in=(,)**|
|not in| **=out=(,)**|
|is null|**=is-null='true\|false'**|


Examples of Pagination criteria :

* return second page and page size equals 50 items max
    - with default naming strategy
    **?PageNumber=2&PageSize=50**
    - with camel case strategy
    **?pageNumber=2&page_size=50**
    - with snake case stategy
    **?page_number=2&page_size=50**
    
Example of Sort criteria

* sort by "name" desc
    - **?sort=name&name.dir=desc**
* sort by "id" and sort by desc year
    - **?sort=id&sort=year&year.dir=desc**

## And ...

* See Autumn.Mvc.Samples project for samples ( http://localhost:5000/swagger ) ...
* See https://github.com/jirutka/rsql-parser
* See http://tools.ietf.org/html/draft-nottingham-atompub-fiql-00
