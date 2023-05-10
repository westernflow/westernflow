package trie 

// create a trie data structure
type Trie struct {
	root *Node
}

// create a node data structure
type Node struct {
	children map[rune]*Node
	isEnd    bool
}

// create a new trie
func NewTrie() *Trie {
	return &Trie{
		root: NewNode(),
	}
}

// create a new node
func NewNode() *Node {
	return &Node{
		children: make(map[rune]*Node),
	}
}

// insert a word into the trie
func (t *Trie) Insert(word string) {
	node := t.root
	for _, char := range word {
		if _, ok := node.children[char]; !ok {
			node.children[char] = NewNode()
		}
		node = node.children[char]
	}
	node.isEnd = true
}

// search a word in the trie
func (t *Trie) Search(word string) bool {
	node := t.root
	for _, char := range word {
		if _, ok := node.children[char]; !ok {
			return false
		}
		node = node.children[char]
	}
	return node.isEnd
}

// starts with prefix
func (t *Trie) StartsWith(prefix string) bool {
	node := t.root
	for _, char := range prefix {
		if _, ok := node.children[char]; !ok {
			return false
		}
		node = node.children[char]
	}
	return true
}

// get all words in the trie
func (t *Trie) GetAllWords() []string {
	words := []string{}
	t.getWords(t.root, "", &words)
	return words
}

// get words recursively
func (t *Trie) getWords(node *Node, word string, words *[]string) {
	if node.isEnd {
		*words = append(*words, word)
	}
	for char, childNode := range node.children {
		t.getWords(childNode, word+string(char), words)
	}
}

// delete a word from the trie
func (t *Trie) Delete(word string) {
	t.delete(t.root, word, 0)
}

// delete recursively
func (t *Trie) delete(node *Node, word string, index int) {
	if index == len(word) {
		node.isEnd = false
		return
	}
	char := rune(word[index])
	if _, ok := node.children[char]; !ok {
		return
	}
	t.delete(node.children[char], word, index+1)
	if len(node.children[char].children) == 0 {
		delete(node.children, char)
	}
}

// get all words with a given prefix
func (t *Trie) GetWordsWithPrefix(prefix string) []string {
	words := []string{}
	node := t.root
	for _, char := range prefix {
		if _, ok := node.children[char]; !ok {
			return words
		}
		node = node.children[char]
	}
	t.getWords(node, prefix, &words)
	return words
}

// get k words with a given prefix
func (t *Trie) GetKWordsWithPrefix(prefix string, k int) []string {
	words := []string{}
	node := t.root
	for _, char := range prefix {
		if _, ok := node.children[char]; !ok {
			return words
		}
		node = node.children[char]
	}
	t.getKWords(node, prefix, &words, k)
	return words
}

// get k words recursively
func (t *Trie) getKWords(node *Node, word string, words *[]string, k int) {
	if node.isEnd {
		*words = append(*words, word)
	}
	if len(*words) == k {
		return
	}
	for char, childNode := range node.children {
		t.getKWords(childNode, word+string(char), words, k)
		if len(*words) == k {
			return
		}
	}
}
