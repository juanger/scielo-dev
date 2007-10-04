require "#{File.dirname(__FILE__)}/../test_helper"

class ArticlesTest < ActionController::IntegrationTest
  fixtures :journals, :journal_issues, :articles

  def setup
    @articles = [:article1, :article2, :article3, :article4, :article5]
    @myarticle = {:language_id => 39, :title => 'Classification of thunderstorm and non-thunderstorm days in Calcutta (India) on the basis of linear discriminant analysis Part 2', :journal_issue_id => 1, :fpage => '12', :lpage => '15', :page_range => '12-15' , :url => 'http://scielo.unam.mx/scielo.php?script=sci_arttext&pid=S0187-62362004000100001&lng=es&nrm=iso&tlng=en', :pacsnum => '12 sss', :other => 'Atmósfera'}
  end

   def test_getting_index
     get "/articles"
     assert_equal 200, status
     assert_equal '/articles', path
   end

   def test_new
     get "/articles/new"
     assert_equal 200, status
     assert_equal '/articles/new', path
   end

   def  test_creating_new_articles
     post "articles/create", :record => @myarticle
     assert_equal 302, status
     follow_redirect!
     assert_equal '/articles/list', path
   end

   def test_showing
     @articles.each { | article |
       post "/articles/show", :id => articles(article).id
       assert 200, status
       assert_equal '/articles/show', path
     }
   end

   def test_editing
     @articles.each { | article |
       post "/articles/edit", :id => articles(article).id
       assert 200, status
       assert "/articles/edit/#{articles(article).id}", path
     }
   end

   def test_updating_name
     @articles.each { | article |
       post "/articles/update", :id => articles(article).id, :title => articles(article).title.reverse
       assert 302, status
       follow_redirect!
       assert "/articles/show/#{articles(article).id}", path
     }
   end

    def test_deleting
     @articles.each { | article |
       get "/articles/destroy", :id => articles(article).id
       assert 302, status
       assert '/articles/list',  path
     }
   end
end
