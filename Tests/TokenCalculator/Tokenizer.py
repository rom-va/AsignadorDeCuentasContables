import transformers

def calculate_text(text):

        chat_tokenizer_dir = "./"    
        tokenizer = transformers.AutoTokenizer.from_pretrained( 
        chat_tokenizer_dir, trust_remote_code=True
        )
        
        return len(tokenizer.encode(text))