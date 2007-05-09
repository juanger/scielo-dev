module CiteIndexHelper
  def apply_article_style(record, style="vancouver")
    author_style(record.authors) + ', ' + record.title + ', ' + journal_style(record) +
    ', ' + journal_issue_style(record)
  end

  def author_style(authors)
    authors.collect { |author|
      author_name = [ author.lastname]
      author_name <<  author.firstname.first.upcase +
        author.middlename.split(' ').collect { |name| name.first }.flatten.to_s
      author_name.join(' ')
    }.join(', ')
  end

  def journal_style(article)
    article.journal_issue.journal.title
  end

  def journal_issue_style(article)
    article.journal_issue.year.to_s
  end
end
