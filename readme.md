------------------------------------------------------
----------- Limitations of current version -----------
------------------------------------------------------
- Proof of concept limitations for parser simplicity -

1. Generic types work only for basic primitive wscript types:
	- int
	- float
	- name
	- string
	
Which means that with generic type T you can't call any function that accepts some other type(unless it's valid generic function itself), and you cannot pass other types to generic function.
	
function template workingExample(T param)
{
	var vec : Vector;
	var generic : T;
	
	// works, DisplayHudMessage() accepts string type
	GetWitcherPlayer().DisplayHudMessage(param);
	
	// works, otherFunction is valid generic function
	otherFunction(param);
	
	// doesn't work, EngineTimeToFloat() returns float, but accepts EngineTime type parameter
	otherFunction(EngineTimeToFloat(param));

	// doesn't work, Vector type not supported
	otherFunction(vec);
	
	// ...but you can still call non-generic functions with it
	vec = VecNormalize(vec);
	
	// ...which won't work if you try to pass generic type T and the target function accepts not supported type as parameter
	vec = VecNormalize(generic);
	
	// use of arithmetic operators limits you to arithmetic types (int/float) which will make the template unusuable for string or name types
	// this combined with direct call to DisplayHudMessage (accepts only strings) makes the template unusable for any type
	generic = param + 42;
}

function template otherFunction(T param)
{
	// works with any supported type, DisplayHudMessage() accepts string type, param is automatically converted to string
	GetWitcherPlayer().DisplayHudMessage("Other function: " + param);
}
	
2. Generic function can only accept generic parameters and only return generic type or void;
3. Syntax must be followed exactly. (whitespace must match, only single spaces will work, _g_ prefix is required)
4. Only function templates supported. (ultimately class templates should be supported at least for generic containers and wrappers)



------------------------------------------------------
-------------- Function template syntax --------------
------------------------------------------------------

// non-void function template
function template _g_templateName(T parameterName) : T // _g_ prefix is required to avoid conflict with non-generic versions of functions
{
	// function body, must return generic type T
}

// void function template
function template _g_templateName(T parameterName)
{
	// function body, doesn't return anything
}

// multiple generic params are supported
function template _g_templateName(T param1, T param2)
{
	// function body, doesn't return anything
}

// invalid templates:
function template templateName(T parameterName) // missing '_g_' prefix before templateName

function template _g_templateName(T parameterName, int param2) // non generic parameter

function template _g_templateName() // doesn't return anything and has no parameters, nothing to deduce



------------------------------------------------------
------------------ Program Usage ---------------------
------------------------------------------------------

1. Create a "templates" folder in the same folder as the exe.
2. Create files with ".wst" extension inside.
3. Write your templates in the wst files. Every file must contain at least one valid template. If a file has at least one valid template, invalid templates are ignored.
4. Run the generator from cmd/powershell, code will be generated in the respective ".ws" files in "generated" folder.

The process should be even simpler in the "full" version but it works well enough for this proof of concept.