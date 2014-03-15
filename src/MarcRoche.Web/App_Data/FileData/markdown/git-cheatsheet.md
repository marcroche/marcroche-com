#Git Cheatsheet

<div>
	<time class="postinfo left-50 postdate">August 25, 2013</time>
</div>

With all of the GUI's available there are still many times when it is simlper and faster to execute Git commands from the command line.

These are a few commands I find myself using often:

**$ git clone ssh://user@domain.com/repo.git**

Clones, or makes a local copy, of an existing repository

**$ git init**

Creates a new local repository in the current working directory

**$ git status**

Lists all changed files in your working directory

**$ git add [.] | [-p &lt;file&gt;]**

Adds all current changes or adds a file to the next commit

**$ git commit [-a]**

Commits staged changes or all local changes.

**$ git log [-p &lt;file&gt;]**

Show the change history for the repository or a specific file

**$ git branch [&lt;new branch-name&gt;]**

Lists all branches or creates a new branch from the current HEAD

**$ git branch -d &lt;branch-name&gt;**

Deletes the named branch

**$ git checkout &lt;branch-name&gt;**

Move HEAD to 'branch-name'

**$ git merge &lt;branch&gt;**

Merges the branch into the current HEAD

**$ git remote -v**

Show all current remotes

**$ git pull &lt;remote&gt; &lt;branch&gt;**

Download changes and merge into HEAD

**$ git push &lt;remote&gt; &lt;branch&gt;**

Publish local changes to a remote

**$ git push &lt;remote&gt; :&lt;branch&gt;**

Delete a branch on a remote

There are more Git commands than this. But this should have you covered for most activities that you will be doing with Git.