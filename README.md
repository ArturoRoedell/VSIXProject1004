# This project was abandoned because of a lack of interest from the creator.
### Main function: to visualize your code, methods and classes,  interpreted as images in a side window next to the editor;

>#### Project Description:
>There are separate parts of the project that can act independently,
three in all, the BackendSharedLibrary, VizFuncMajorTasks, and finally the vsix plugin which creates the tool window and adds the images.
I am not sure this was a good idea.
the part of the project that is the main workhorse is called BaendSharedLibrary, very descriptive I know.
This piece parses your code to look for classes and methods then searches the web for image URL links appropriately,
and finally downloads images if not downloaded previously. The most crucial part is that it produces a JSON file
to find the correct images for the correct line number of your document.


--------
>More in-depth explanation of why it was abandoned:
>I could not get my VSIX program to tell me anything about the current document in the window. I wasn’t sure what I was missing. I tried everything I could find in the official Windows site and also tried every code snippet related ‘current window text buffer’. Basically, I was sure I could create an entire complex game, worthy of an indie studio, by myself with Unreal Engine before I got anywhere with VS plugins. And therefore that is what I am doing.
