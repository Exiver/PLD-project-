
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF               =  0, // (EOF)
        SYMBOL_ERROR             =  1, // (Error)
        SYMBOL_WHITESPACE        =  2, // Whitespace
        SYMBOL_MINUS             =  3, // '-'
        SYMBOL_MINUSMINUS        =  4, // '--'
        SYMBOL_EXCLAMEQ          =  5, // '!='
        SYMBOL_PERCENT           =  6, // '%'
        SYMBOL_LPAREN            =  7, // '('
        SYMBOL_RPAREN            =  8, // ')'
        SYMBOL_TIMES             =  9, // '*'
        SYMBOL_TIMESTIMES        = 10, // '**'
        SYMBOL_COMMA             = 11, // ','
        SYMBOL_DIV               = 12, // '/'
        SYMBOL_COLON             = 13, // ':'
        SYMBOL_SEMI              = 14, // ';'
        SYMBOL_LBRACE            = 15, // '{'
        SYMBOL_RBRACE            = 16, // '}'
        SYMBOL_PLUS              = 17, // '+'
        SYMBOL_PLUSPLUS          = 18, // '++'
        SYMBOL_LT                = 19, // '<'
        SYMBOL_LTEQ              = 20, // '<='
        SYMBOL_EQ                = 21, // '='
        SYMBOL_EQEQ              = 22, // '=='
        SYMBOL_GT                = 23, // '>'
        SYMBOL_GTEQ              = 24, // '>='
        SYMBOL_BREAK             = 25, // break
        SYMBOL_CASE              = 26, // case
        SYMBOL_DEFAULT           = 27, // default
        SYMBOL_DIGIT             = 28, // Digit
        SYMBOL_DO                = 29, // do
        SYMBOL_DOUBLE            = 30, // double
        SYMBOL_ELSE              = 31, // else
        SYMBOL_END               = 32, // End
        SYMBOL_FLOAT             = 33, // float
        SYMBOL_FOR               = 34, // for
        SYMBOL_FUNCTION          = 35, // function
        SYMBOL_ID                = 36, // id
        SYMBOL_IF                = 37, // if
        SYMBOL_INT               = 38, // int
        SYMBOL_RETURN            = 39, // return
        SYMBOL_START             = 40, // Start
        SYMBOL_STRING            = 41, // string
        SYMBOL_SWITCH            = 42, // switch
        SYMBOL_WHILE             = 43, // while
        SYMBOL_ASSIGN            = 44, // <assign>
        SYMBOL_CASE2             = 45, // <case>
        SYMBOL_CASES             = 46, // <cases>
        SYMBOL_CONCEPT           = 47, // <concept>
        SYMBOL_COND              = 48, // <cond>
        SYMBOL_DATA              = 49, // <data>
        SYMBOL_DEFAULT2          = 50, // <default>
        SYMBOL_DIGIT2            = 51, // <digit>
        SYMBOL_DOMINUSWHILE_STMT = 52, // <do-while_stmt>
        SYMBOL_EXP               = 53, // <exp>
        SYMBOL_EXPR              = 54, // <expr>
        SYMBOL_FACTOR            = 55, // <factor>
        SYMBOL_FOR_STMT          = 56, // <for_stmt>
        SYMBOL_FUNCTION2         = 57, // <function>
        SYMBOL_ID2               = 58, // <id>
        SYMBOL_IF_STMT           = 59, // <if_stmt>
        SYMBOL_OPR               = 60, // <opr>
        SYMBOL_PARAMETERS        = 61, // <parameters>
        SYMBOL_PROGRAM           = 62, // <program>
        SYMBOL_RETURN2           = 63, // <return>
        SYMBOL_STEP              = 64, // <step>
        SYMBOL_STMT_LIST         = 65, // <stmt_List>
        SYMBOL_SWITCH_STMT       = 66, // <switch_stmt>
        SYMBOL_TERM              = 67, // <term>
        SYMBOL_WHILE_STMT        = 68  // <while_stmt>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_END                                         =  0, // <program> ::= Start <stmt_List> End
        RULE_STMT_LIST                                                 =  1, // <stmt_List> ::= <concept>
        RULE_STMT_LIST2                                                =  2, // <stmt_List> ::= <concept> <stmt_List>
        RULE_CONCEPT                                                   =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                                  =  4, // <concept> ::= <if_stmt>
        RULE_CONCEPT3                                                  =  5, // <concept> ::= <for_stmt>
        RULE_CONCEPT4                                                  =  6, // <concept> ::= <switch_stmt>
        RULE_CONCEPT5                                                  =  7, // <concept> ::= <while_stmt>
        RULE_CONCEPT6                                                  =  8, // <concept> ::= <do-while_stmt>
        RULE_CONCEPT7                                                  =  9, // <concept> ::= <function>
        RULE_ASSIGN_EQ_SEMI                                            = 10, // <assign> ::= <id> '=' <expr> ';'
        RULE_ID_ID                                                     = 11, // <id> ::= id
        RULE_EXPR_PLUS                                                 = 12, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                                = 13, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                                      = 14, // <expr> ::= <term>
        RULE_TERM_TIMES                                                = 15, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                                  = 16, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                              = 17, // <term> ::= <term> '%' <factor>
        RULE_TERM                                                      = 18, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                         = 19, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                                    = 20, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                         = 21, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                       = 22, // <exp> ::= <id>
        RULE_EXP2                                                      = 23, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                               = 24, // <digit> ::= Digit
        RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACE_RBRACE                    = 25, // <if_stmt> ::= if '(' <cond> ')' '{' <stmt_List> '}'
        RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE = 26, // <if_stmt> ::= if '(' <cond> ')' '{' <stmt_List> '}' else '{' <stmt_List> '}'
        RULE_COND                                                      = 27, // <cond> ::= <expr> <opr> <expr>
        RULE_OPR_LT                                                    = 28, // <opr> ::= '<'
        RULE_OPR_GT                                                    = 29, // <opr> ::= '>'
        RULE_OPR_EQEQ                                                  = 30, // <opr> ::= '=='
        RULE_OPR_EXCLAMEQ                                              = 31, // <opr> ::= '!='
        RULE_OPR_GTEQ                                                  = 32, // <opr> ::= '>='
        RULE_OPR_LTEQ                                                  = 33, // <opr> ::= '<='
        RULE_FOR_STMT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE        = 34, // <for_stmt> ::= for '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <stmt_List> '}'
        RULE_DATA_INT                                                  = 35, // <data> ::= int
        RULE_DATA_FLOAT                                                = 36, // <data> ::= float
        RULE_DATA_DOUBLE                                               = 37, // <data> ::= double
        RULE_DATA_STRING                                               = 38, // <data> ::= string
        RULE_STEP_MINUSMINUS                                           = 39, // <step> ::= '--' <id>
        RULE_STEP_MINUSMINUS2                                          = 40, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS                                             = 41, // <step> ::= '++' <id>
        RULE_STEP_PLUSPLUS2                                            = 42, // <step> ::= <id> '++'
        RULE_STEP                                                      = 43, // <step> ::= <assign>
        RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE            = 44, // <switch_stmt> ::= switch '(' <exp> ')' '{' <cases> <default> '}'
        RULE_CASES                                                     = 45, // <cases> ::= <case> <cases>
        RULE_CASES2                                                    = 46, // <cases> ::= <case>
        RULE_CASE_CASE_COLON_LBRACE_BREAK_SEMI_RBRACE                  = 47, // <case> ::= case <exp> ':' '{' <stmt_List> break ';' '}'
        RULE_DEFAULT_DEFAULT_COLON_BREAK_SEMI                          = 48, // <default> ::= default ':' <stmt_List> break ';'
        RULE_WHILE_STMT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE              = 49, // <while_stmt> ::= while '(' <cond> ')' '{' <stmt_List> '}'
        RULE_DOWHILE_STMT_DO_LBRACE_RBRACE_WHILE_LPAREN_RPAREN_SEMI    = 50, // <do-while_stmt> ::= do '{' <stmt_List> '}' while '(' <cond> ')' ';'
        RULE_FUNCTION_FUNCTION_LPAREN_RPAREN_LBRACE_RBRACE             = 51, // <function> ::= <data> function <id> '(' <parameters> ')' '{' <stmt_List> <return> '}'
        RULE_PARAMETERS_COMMA                                          = 52, // <parameters> ::= <id> ',' <parameters>
        RULE_PARAMETERS                                                = 53, // <parameters> ::= <id>
        RULE_RETURN_RETURN_SEMI                                        = 54, // <return> ::= return <exp> ';'
        RULE_RETURN_RETURN_SEMI2                                       = 55  // <return> ::= return ';'
    };

    public class MyParser
    {
        ListBox lst;
        private LALRParser parser;

        public MyParser(string filename,ListBox lst)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);

            this.lst = lst;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BREAK :
                //break
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //default
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DO :
                //do
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTION :
                //function
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURN :
                //return
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE2 :
                //<case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASES :
                //<cases>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT2 :
                //<default>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOMINUSWHILE_STMT :
                //<do-while_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FUNCTION2 :
                //<function>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OPR :
                //<opr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMETERS :
                //<parameters>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURN2 :
                //<return>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_List>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_STMT :
                //<switch_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE_STMT :
                //<while_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_END :
                //<program> ::= Start <stmt_List> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_List> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_List> ::= <concept> <stmt_List>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT4 :
                //<concept> ::= <switch_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT5 :
                //<concept> ::= <while_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT6 :
                //<concept> ::= <do-while_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT7 :
                //<concept> ::= <function>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_SEMI :
                //<assign> ::= <id> '=' <expr> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<if_stmt> ::= if '(' <cond> ')' '{' <stmt_List> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_LBRACE_RBRACE_ELSE_LBRACE_RBRACE :
                //<if_stmt> ::= if '(' <cond> ')' '{' <stmt_List> '}' else '{' <stmt_List> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <opr> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPR_LT :
                //<opr> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPR_GT :
                //<opr> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPR_EQEQ :
                //<opr> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPR_EXCLAMEQ :
                //<opr> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPR_GTEQ :
                //<opr> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OPR_LTEQ :
                //<opr> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE :
                //<for_stmt> ::= for '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <stmt_List> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_INT :
                //<data> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_FLOAT :
                //<data> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DOUBLE :
                //<data> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_STRING :
                //<data> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE :
                //<switch_stmt> ::= switch '(' <exp> ')' '{' <cases> <default> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASES :
                //<cases> ::= <case> <cases>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASES2 :
                //<cases> ::= <case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_CASE_COLON_LBRACE_BREAK_SEMI_RBRACE :
                //<case> ::= case <exp> ':' '{' <stmt_List> break ';' '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DEFAULT_DEFAULT_COLON_BREAK_SEMI :
                //<default> ::= default ':' <stmt_List> break ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILE_STMT_WHILE_LPAREN_RPAREN_LBRACE_RBRACE :
                //<while_stmt> ::= while '(' <cond> ')' '{' <stmt_List> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DOWHILE_STMT_DO_LBRACE_RBRACE_WHILE_LPAREN_RPAREN_SEMI :
                //<do-while_stmt> ::= do '{' <stmt_List> '}' while '(' <cond> ')' ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FUNCTION_FUNCTION_LPAREN_RPAREN_LBRACE_RBRACE :
                //<function> ::= <data> function <id> '(' <parameters> ')' '{' <stmt_List> <return> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERS_COMMA :
                //<parameters> ::= <id> ',' <parameters>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERS :
                //<parameters> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURN_RETURN_SEMI :
                //<return> ::= return <exp> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RETURN_RETURN_SEMI2 :
                //<return> ::= return ';'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + " in line: " + args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 = "Expected token: "+args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }

    }
}
