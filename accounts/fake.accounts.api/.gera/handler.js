module.exports  = {
    name:'handler',
    description:'',
    run:run
}

/*
    Complete toolbox documentation:
    https://infinitered.github.io/gluegun/#/toolbox-api
*/

function run(toolbox){
    
    //Pega e valida o nome do handler
    const nome = toolbox.parameters.first;

    if(typeof nome !== "string"){
        toolbox.print.error('First parameter not entered!')
        return;
    }

    //Cria o arquivo de interface da handler
    toolbox.gera.generate({
        template: 'ihandler.ejs',
        target: `src/Accounts.Core/Handlers/I${nome}Handler.cs`,
        props: { 
            nome:nome,
        }
    })

    //Cria o arquivo referente a handler
    toolbox.gera.generate({
        template: 'handler.ejs',
        target: `src/Accounts.Application/Handlers/${nome}Handler.cs`,
        props: { 
            nome:nome,
        }
    })
    
    //Adiciona a injeção de dependência na classe Startup.cs
    const addTransient = `            services.AddScoped<I${nome}Handler,${nome}Handler>();`;

    toolbox.patching.patch('src/Accounts.API/Configurations/DependencyInjectionConfiguration.cs', { 
        insert: `\n${addTransient}`, 
        after: 'GERA-COMMANDS-ADD-HANDLER' 
    })
}

/*
    TOOLBOX PARAMETERS - toolbox.parameters.[name_field] :
        name	    type	    purpose	                                from the example above
        -------------------------------------------------------------------------------------------------
        plugin	    string	    the plugin used	                        'reactotron'
        command	    string	    the command used	                    'plugin'
        string	    string	    the command arguments as a string	    'MyAwesomePlugin full'
        array	    array	    the command arguments as an array	    ['MyAwesomePlugin', 'full']
        first	    string	    the 1st argument	                    'MyAwesomePlugin'
        second	    string	    the 2nd argument	                    'full'
        third	    string	    the 3rd argument	                    undefined
        options	    object	    command line options	                {comments: true, lint: 'standard'}
        argv	    object	    raw                                     argv	

*/

/*
    TOOLBOX TEMPLATE GENERATE - toolbox.template.generate({...}) 
        option	    type	    purpose	                                notes
        -----------------------------------------------------------------------------------------------------------
        template	string	    path to the EJS template	            relative from plugin's templates directory
        target	    string	    path to create the file	                relative from user's working directory
        props	    object	    more data to render in your template	
        directory	string	    where to find templates	                an absolute path (optional)
*/