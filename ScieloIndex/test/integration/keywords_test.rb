require "#{File.dirname(__FILE__)}/../test_helper"

class KeywordsTest < ActionController::IntegrationTest
  fixtures :keywords

  def setup
    @keywords = [:quakes, :storms, :eruptions, :floods]
    @mykeyword = {:name => 'tornados'}
  end

   def test_getting_index
     get "/keywords"
     assert_equal 200, status
     assert_equal '/keywords', path
   end

   def test_showing
     @keywords.each { | keyword |
       post "/keywords/show", :id => keywords(keyword).id
       assert 200, status
       assert_equal '/keywords/show', path
     }
   end

   def test_editing
     @keywords.each { | keyword |
       post "/keywords/edit", :id => keywords(keyword).id
       assert 200, status
       assert "/keywords/edit/#{keywords(keyword).id}", path
     }
   end

   def test_updating_name
     @keywords.each { | keyword |
       post "/keywords/update", :id => keywords(keyword).id, :parent => keywords(keyword).name.reverse
       assert 302, status
       follow_redirect!
       assert "/keywords/show/#{keywords(keyword).id}", path
     }
   end

    def test_deleting
     @keywords.each { | keyword |
       get "/keywords/destroy", :id => keywords(keyword).id
       assert 302, status
       assert '/keywords/list',  path
     }
   end
end
