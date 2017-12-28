CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'clipboard', groups: ['undo', 'clipboard'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'insert', groups: ['insert'] },
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'others', groups: ['others'] },
		{ name: 'about', groups: ['about'] }
    ];
    config.enterMode = CKEDITOR.ENTER_BR;
    config.height = 530;
    config.colorButton_enableAutomatic = false;
    config.colorButton_enableMore = false;
    config.colorButton_colors = '000,fff,C01900,FF2500,FFC003,FFFB00,91D051,00AF50,00B1F0,0070C0,002160,7130A0';
    config.removeButtons = 'Source,Save,Print,Preview,NewPage,SelectAll,Templates,PasteFromWord,PasteText,Paste,Copy,Cut,Scayt,Form,TextField,Textarea,Select,Button,ImageButton,HiddenField,Radio,RemoveFormat,Checkbox,CreateDiv,Language,Anchor,Image,Flash,Table,HorizontalRule,Smiley,SpecialChar,PageBreak,Iframe,Maximize,ShowBlocks,About,Styles,Font,Link,Unlink,BidiLtr,BidiRtl,Find,Replace,Blockquote,Undo,Redo,Superscript,Subscript,Italic,Outdent,Indent,Format';
    config.removePlugins = "magicline";
};