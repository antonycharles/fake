module.exports  = {
    name:'controller',
    description:'',
    run:run
}

/*
    Complete toolbox documentation:
    https://infinitered.github.io/gluegun/#/toolbox-api
*/

function run(toolbox){
    
    const classe_name = toolbox.parameters.first;

    if(typeof classe_name !== "string"){
        toolbox.print.error('Nome da classe não informado!')
        return;
    }

    toolbox.gera.generate({
        template: 'controller.ejs',
        target: `src/Accounts.API/Controllers/${classe_name}Controller.cs`,
        props: { 
            name:classe_name,
        }
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