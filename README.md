# cssh
The C S(harp) Sh(ell)!

## Project Goals
* To gain experience with C#
* To gain experience with various Windows development tools
* To gain experience with the Windows shell environment and OS environment; and
* To have some fun!

## Design

cssh will start out with a simple design, merely executing programs and waiting until they terminate like so:

```bash
$ cssh
cssh> echo "Hello!"
Hello!
cssh> exit
$
```

For now, this doesn't include but may extend into:
* IO redirection (>, >>, <, << and | equivalents)
* Running in the background (&)
* Logical operators (&&, ||)
* scripting flow control (while and for loops, if statements)
* variables (e.g. ``CURRDIR=`pwd` ``)
* Other fancy shell functionality

## How do you expect people to use this shell?

I don't! Pleast don't actually use this over the many much better shells out there, this is just a fun learning project :)

Although if you do think my shell is good, please let me know it would make my day <3
